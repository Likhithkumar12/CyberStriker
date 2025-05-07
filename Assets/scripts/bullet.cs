using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] GameObject impactEffect;
    private void OnCollisionEnter(Collision collision)
    {
        playimpacteffect(collision);
       Objectpool.instance.returnobject(0.1f, gameObject);
        
    }
    private void playimpacteffect(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
            GameObject impact = Objectpool.instance.getObject(impactEffect);
            impact.transform.position = contact.point;
            Objectpool.instance.returnobject(1f, impact);
        }

    }
}
