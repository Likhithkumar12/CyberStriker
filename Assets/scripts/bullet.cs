using UnityEngine;

public class bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        // GetComponent<Rigidbody>().constraints= RigidbodyConstraints.FreezeAll;
        Destroy(gameObject);
            }
}
