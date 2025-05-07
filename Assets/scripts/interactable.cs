using UnityEngine;

public class interactable : MonoBehaviour
{
    Material defaultmaterial;
    [SerializeField] Material highlightmaterial;
    MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        defaultmaterial = meshRenderer.material;
    }
    public virtual void interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
        if (playerInteraction != null)
        {

            playerInteraction.interactables.Add(this);
            playerInteraction.closestandhighlightcolor();
        }

    }
    protected virtual void OnTriggerExit(Collider other)
    {
        PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
        if (playerInteraction != null)
        {

            playerInteraction.interactables.Remove(this);
            playerInteraction.closestandhighlightcolor();
        }
    }
    public void highlightcolr(bool active)
    {
        if(active)
        {
            meshRenderer.material = highlightmaterial;
        }
        else
        {
            meshRenderer.material = defaultmaterial;
        }
    }
}
