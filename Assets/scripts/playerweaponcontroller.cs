
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEditor.ShaderKeywordFilter;
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
    private void Start()
    {
        player = GetComponent<player>();
        assigninputactions();
        Invoke("startweapon", 0.1f);

    }
    void Update()
    {
        if (isshooting)
        {
            Shoot();
        }
        
    }
    public void startweapon()
    {
        switchgun(0);
    }
    public bool Weaponready()=>weaponready;
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
        inputactions.Character.Fire.performed += Context =>isshooting = true;
        inputactions.Character.Fire.canceled += Context => isshooting = false;
       
        inputactions.Character.slot1.performed += Context => switchgun(0);
        inputactions.Character.slot2.performed += Context => switchgun(1);
        inputactions.Character.dropgun.performed += Context => dropgun();
        inputactions.Character.reload.performed += Context =>
        {
            if (currentweapon.weaponcanreload()&& weaponready)
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
    public bool onlyonebackup()=> weaponlist.Count <= 1;
    public void pickupweapon(weapon weapon)
    {
        if (weaponlist.Count >= 2)
        {
            return;
        }
        weaponlist.Add(weapon);
        player.weapnvisualcontroller.switchonbackup();

    }


    private void Shoot()
    {
        if (currentweapon.canshoot() == false)
        {
            return;
        }
        if(weaponready == false)
        {
            return;
        }
        if (currentweapon.shootType == ShootType.Single)
        {
            isshooting = false;
            
        }
        currentweapon.inmagazineammo--;
        GameObject bullet= Objectpool.instance.getbullet();
        bullet.transform.position = returngunpoint();
        bullet.transform.rotation = Quaternion.LookRotation(BulletDirection());
        Rigidbody newbulletrb = bullet.GetComponent<Rigidbody>();
        newbulletrb.mass = REFERENCE_BULLET_SPEED / bulletSpeed;
        newbulletrb.linearVelocity = BulletDirection() * bulletSpeed;
        GetComponentInChildren<Animator>().SetTrigger("shoot");

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
