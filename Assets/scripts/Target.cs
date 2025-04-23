using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{

    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("obstacles");

    }


    void Update()
    {

    }
}
