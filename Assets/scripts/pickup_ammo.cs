using UnityEngine;

public class pickup_ammo : interactable
{
    public override void interact()
    {
        base.interact();

        Debug.Log("Picked up ammo from " + gameObject.name);
    }
    
}
