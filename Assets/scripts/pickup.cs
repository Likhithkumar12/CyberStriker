using UnityEngine;

public class pickup : MonoBehaviour
{
    [SerializeField] weapon weapon;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<playerweaponcontroller>()?.pickupweapon(weapon);
      

    }
}
