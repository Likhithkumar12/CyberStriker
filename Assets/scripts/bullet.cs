using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] GameObject impactEffect;
    private void OnCollisionEnter(Collision collision)
    {

        // GetComponent<Rigidbody>().constraints= RigidbodyConstraints.FreezeAll;
        playimpacteffect(collision);
        Destroy(gameObject);
    }
    private void playimpacteffect(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
           GameObject impact = Instantiate(impactEffect, contact.point, Quaternion.LookRotation(contact.normal));
            Destroy(impact, .5f);
        }
       
    }
}
