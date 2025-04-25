
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering.LookDev;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class playerweaponcontroller : MonoBehaviour
{
    player player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunpoint;
    [SerializeField] Transform Weaponholder;
    [SerializeField] Transform Aimm;
    [SerializeField] float bulletSpeed = 20f;
    private const float REFERENCE_BULLET_SPEED = 20f;


    [SerializeField]  public weapon currentweapon;
    [Header("inventroy")]
    [SerializeField] List<weapon> weaponlist;


    private void Start()
    {
        player = GetComponent<player>();
        assigninputactions();
    }
    public weapon Currentweapon() => currentweapon;

    private void assigninputactions()
    {
        Input inputactions = player.inputactions;
        inputactions.Character.Fire.performed += Context =>
        {
            if (!player.weapnvisualcontroller.weponisbusy) Shoot();
        };
        inputactions.Character.slot1.performed += Context => switchgun(0);
        inputactions.Character.slot2.performed += Context => switchgun(1);
        inputactions.Character.dropgun.performed += Context => dropgun();
        inputactions.Character.reload.performed += Context =>
        {
            if (currentweapon.weaponcanreload())
                player.weapnvisualcontroller.playreloadanimations();
        };

    }
    private void switchgun(int index)
    {
        currentweapon = weaponlist[index];

    }
    private void dropgun()
    {
        if (weaponlist.Count <= 1)
        {
            return;
        }
        weaponlist.Remove(currentweapon);
        currentweapon = weaponlist[0];
    }
    public void pickupweapon(weapon weapon)
    {
        if (weaponlist.Count >= 2)
        {
            return;
        }
        weaponlist.Add(weapon);

    }


    private void Shoot()
    {
        if (currentweapon.canshoot() == false)
        {
            return;
        }
        currentweapon.inmagazineammo--;
        GameObject bullet = Instantiate(bulletPrefab, gunpoint.position, Quaternion.LookRotation(BulletDirection()));
        Rigidbody newbulletrb = bullet.GetComponent<Rigidbody>();
        newbulletrb.mass = REFERENCE_BULLET_SPEED / bulletSpeed;
        newbulletrb.linearVelocity = BulletDirection() * bulletSpeed;
        Destroy(bullet, 5f);
        GetComponentInChildren<Animator>().SetTrigger("shoot");

    }
    public Vector3 returngunpoint() => gunpoint.position;
    public Vector3 BulletDirection()
    {
        Vector3 direction = (Aimm.position - gunpoint.position).normalized;
        if (!player.playeraimm.Isaimingprecise())
        {
            direction.y = 0;
        }
        Weaponholder.LookAt(Aimm.position);
        gunpoint.LookAt(Aimm.position);
        return direction;
    }
}
