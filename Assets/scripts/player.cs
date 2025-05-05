using UnityEngine;

public class player : MonoBehaviour
{
    public Input inputactions {get; private set;}
    public playeraim playeraimm { get; private set; }
    public weapnvisualcontroller weapnvisualcontroller { get; private set;}
    public playerweaponcontroller playerweaponcontroller { get; private set; }
    void Awake()
    {
        inputactions = new Input();
        playeraimm = GetComponent<playeraim>();
        playerweaponcontroller = GetComponent<playerweaponcontroller>();
        weapnvisualcontroller = GetComponent<weapnvisualcontroller>();
    }
    void OnEnable()
    {
        inputactions.Enable();

    }
    void OnDisable()
    {

        inputactions.Disable();
    }
}
