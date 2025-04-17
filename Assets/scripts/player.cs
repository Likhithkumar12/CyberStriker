using UnityEngine;

public class player : MonoBehaviour
{
    public Input inputactions;
    void Awake()
    {
        inputactions = new Input();
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
