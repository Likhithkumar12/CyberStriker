using UnityEngine;

public class player : MonoBehaviour
{
    public Input inputactions {get; private set;}
    public playeraim playeraimm { get; private set; }
    public weapnvisualcontroller weapnvisualcontroller { get; private set; }
    void Awake()
    {
        inputactions = new Input();
        playeraimm = GetComponent<playeraim>();
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
