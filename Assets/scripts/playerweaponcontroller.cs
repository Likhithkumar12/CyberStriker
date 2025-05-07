
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerweaponcontroller : MonoBehaviour
{
    player player;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] Transform Weaponholder;
    [SerializeField] Transform Aimm;
    [SerializeField] float bulletSpeed = 20f;
    private const float REFERENCE_BULLET_SPEED = 20f;


    [SerializeField] public weapon currentweapon;
    [Header("inventory")]
    [SerializeField] List<weapon> weaponlist;
    private bool weaponready;
    private bool isshooting;
    private bool hasFiredBurst = true;
    private void Start()
    {
        player = GetComponent<player>();
        assigninputactions();
        Invoke("startweapon", 0.1f);

    }
    void Update()
    {
        if (isshooting && hasFiredBurst && Weaponready())
        {
            Shoot();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.T))
        {
            currentweapon.toggleburstfire();

        }

    }
    public void startweapon()
    {
        switchgun(0);
    }
    public bool Weaponready() => weaponready;
    public void setweaponready(bool value)
    {
        weaponready = value;
    }
    public weapon Currentweapon() => currentweapon;

    public weapon Backupweapon()
    {
        foreach (weapon weapon in weaponlist)
        {
            if (weapon != currentweapon)
            {
                return weapon;
            }
        }
        return null;
    }

    private void assigninputactions()
    {
        Input inputactions = player.inputactions;
        inputactions.Character.Fire.performed += Context =>
        {
            if (!Weaponready()) return;

            if (currentweapon.isActive && currentweapon.canshoot())
            {
                if (hasFiredBurst)
                {
                    hasFiredBurst = false;
                    StartCoroutine(BurstFire());
                }
            }
            else
            {
                isshooting = true;
                Shoot();
            }
        };
        inputactions.Character.Fire.canceled += Context =>
    {
        isshooting = false;
        hasFiredBurst = true;

    };

        inputactions.Character.slot1.performed += Context => switchgun(0);
        inputactions.Character.slot2.performed += Context => switchgun(1);
        inputactions.Character.slot3.performed += Context => switchgun(2);
        inputactions.Character.dropgun.performed += Context => dropgun();
        inputactions.Character.reload.performed += Context =>
        {
            if (currentweapon.weaponcanreload() && weaponready)
            {
                setweaponready(false);
                player.weapnvisualcontroller.playreloadanimations();
            }

        };
    }
    private void switchgun(int index)
    {
        setweaponready(false);
        currentweapon = weaponlist[index];
        player.weapnvisualcontroller.weaponequipanimation();



    }
    private void dropgun()
    {
        if (onlyonebackup())
        {
            return;
        }
        weaponlist.Remove(currentweapon);
        switchgun(0);
    }
    public bool onlyonebackup() => weaponlist.Count <= 1;
    public bool hasbackupininventory(WeaponType weaponType)
    {
        foreach (weapon weapon in weaponlist)
        {
            if (weapon.weaponType == weaponType)
            {
                return true;
            }
        }
        return false;
    }
    public void pickupweapon(weapon weapon)
    {
        if (weaponlist.Count >= 2)
        {
            return;
        }
        weaponlist.Add(weapon);
        player.weapnvisualcontroller.switchonbackup();

    }
    private IEnumerator BurstFire()
    {
        setweaponready(false);

        for (int i = 1; i <= currentweapon.bulletsperburst; i++)
        {
            FireSingleBullet();
            yield return new WaitForSeconds(currentweapon.burstdelay);
        }

        hasFiredBurst = true;
        setweaponready(true);
    }

    private void Shoot()
    {
        if (!Weaponready()) return;
        if (!currentweapon.canshoot()) return;

        GetComponentInChildren<Animator>().SetTrigger("shoot");

        if (currentweapon.shootType == ShootType.Single)
        {
            isshooting = false;
        }
        FireSingleBullet();
    }


    private void FireSingleBullet()
    {
        currentweapon.inmagazineammo--;
        GameObject bullet = Objectpool.instance.getObject(bulletPrefab);
        bullet.transform.position = returngunpoint();
        bullet.transform.rotation = Quaternion.LookRotation(BulletDirection());

        Rigidbody newbulletrb = bullet.GetComponent<Rigidbody>();
        Vector3 bulletDirection = currentweapon.ApplySpread(BulletDirection());
        newbulletrb.mass = REFERENCE_BULLET_SPEED / bulletSpeed;
        newbulletrb.linearVelocity = bulletDirection * bulletSpeed;
    }

    public Vector3 returngunpoint() => player.weapnvisualcontroller.currentweaponmodel().gunpoint.position;
    public Vector3 BulletDirection()
    {
        Vector3 direction = (Aimm.position - returngunpoint()).normalized;
        if (!player.playeraimm.Isaimingprecise())
        {
            direction.y = 0;
        }

        return direction;
    }
}
