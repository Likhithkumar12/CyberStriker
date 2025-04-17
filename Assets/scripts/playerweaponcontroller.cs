using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class playerweaponcontroller : MonoBehaviour
{
    player player;
    private void Start()
    {
        player = GetComponent<player>();
        player.inputactions.Character.Fire.performed += Context => Shoot();
    }
    private void Shoot()
    {
        GetComponentInChildren<Animator>().SetTrigger("shoot");

    }
}
