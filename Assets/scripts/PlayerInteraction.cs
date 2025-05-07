using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public List<interactable> interactables = new List<interactable>();
    public interactable closestInteractable;
    player player;
    void Awake()
    {
        player = GetComponent<player>();
    }
    void Start()
    {
        player.inputactions.Character.pickup.performed += ctx => interactiwith();
    }



    public void interactiwith()
    {
        closestInteractable?.interact();
    }


    public void closestandhighlightcolor()
    {
        closestInteractable?.highlightcolr(false);
        closestInteractable = null;
        float closestDistance = float.MaxValue;
        foreach (interactable interactable in interactables)
        {
            float distance = Vector3.Distance(transform.position, interactable.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
            }
        }
        closestInteractable?.highlightcolr(true);
    }
}
