using UnityEngine;

public class pickup_weapon : interactable
{
    playerweaponcontroller playerweaponcontroller;
    [SerializeField] WeaponData weaponData;
    public override void interact()
    {
        playerweaponcontroller.pickupweapon(weaponData);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        playerweaponcontroller = other.GetComponent<playerweaponcontroller>();
    }
   
}
