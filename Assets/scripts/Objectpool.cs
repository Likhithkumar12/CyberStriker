using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class Objectpool : MonoBehaviour
{
    public static Objectpool instance;
    Dictionary<GameObject, Queue<GameObject>> globalpool = new Dictionary<GameObject, Queue<GameObject>>();
    [SerializeField] int poolsize = 100;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void InitialllzePool(GameObject prefab)
    {
        globalpool[prefab] = new Queue<GameObject>();
        for (int i = 0; i < poolsize; i++)
        {
            createnewObject(prefab);
        }
    }

    private void createnewObject(GameObject prefab)
    {
        GameObject newobject = Instantiate(prefab, transform);
        newobject.AddComponent<pooledobject>().gameObject = prefab;
        newobject.SetActive(false);
        globalpool[prefab].Enqueue(newobject);
    }
    public void returnobject(float delay, GameObject prefab)
    {
        StartCoroutine(Returndelay(delay, prefab));
    }
    private IEnumerator Returndelay(float delay, GameObject prefab)
    {
        yield return new WaitForSeconds(delay);
        returntopool(prefab);
    }

    private void returntopool(GameObject prefab)
    {
        GameObject returnobject = prefab.GetComponent<pooledobject>().gameObject;
        prefab.SetActive(false);
        prefab.transform.parent = transform;
        globalpool[returnobject].Enqueue(prefab);

    }
    public GameObject getObject(GameObject objectt)
    {
        if (globalpool.ContainsKey(objectt) == false)
        {
            InitialllzePool(objectt);

        }
        if (globalpool[objectt].Count == 0)
        {
            createnewObject(objectt);
        }
        GameObject obejcttoget = globalpool[objectt].Dequeue();
        obejcttoget.SetActive(true);
        obejcttoget.transform.parent = null;
        return obejcttoget;

    }
}

