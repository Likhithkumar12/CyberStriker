using UnityEngine;

public class player : MonoBehaviour
{
    public Input inputactions {get; private set;}
    public playeraim playeraimm { get; private set; }
    void Awake()
    {
        inputactions = new Input();
        playeraimm = GetComponent<playeraim>();
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
