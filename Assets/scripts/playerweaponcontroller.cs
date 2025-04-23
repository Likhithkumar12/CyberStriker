
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
    weapnvisualcontroller weapnvisualcontroller;
    playeraim playeraimm;

    private void Start()
    {
        player = GetComponent<player>();
        playeraimm = GetComponent<playeraim>();
        weapnvisualcontroller = GetComponentInChildren<weapnvisualcontroller>();
        player.inputactions.Character.Fire.performed += Context =>
        {
            if (!weapnvisualcontroller.weponisbusy) Shoot();
        };
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunpoint.position, Quaternion.LookRotation(BulletDirection()));
        bullet.GetComponent<Rigidbody>().linearVelocity = BulletDirection() * bulletSpeed; 
        Destroy(bullet, 5f);
        GetComponentInChildren<Animator>().SetTrigger("shoot");

    }
    public Vector3 returngunpoint() => gunpoint.position;
    public  Vector3 BulletDirection()
    {
        Vector3 direction = (Aimm.position - gunpoint.position).normalized;
        if (!playeraimm.isaimingprecise())
        {
            direction.y = 0;
        }
        Weaponholder.LookAt(Aimm.position);
        gunpoint.LookAt(Aimm.position);
        return direction;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(Weaponholder.position, Weaponholder.position + BulletDirection()* 25);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gunpoint.position, gunpoint.position + BulletDirection() * 25);

    }
}
