
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class playerweaponcontroller : MonoBehaviour
{
    player player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunpoint;
    [SerializeField] float bulletSpeed = 20f;

    private void Start()
    {
        player = GetComponent<player>();
        player.inputactions.Character.Fire.performed += Context => Shoot();
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunpoint.position, Quaternion.LookRotation(transform.forward));
        bullet.GetComponent<Rigidbody>().linearVelocity = transform.forward * bulletSpeed; 
        Destroy(bullet, 5f); 
        GetComponentInChildren<Animator>().SetTrigger("shoot");

    }
}
