using System.Collections.Generic;
using UnityEngine;


public class Objectpool : MonoBehaviour
{
    public static Objectpool instance;
    [SerializeField] GameObject bulletPrefab;
    Queue<GameObject> bulletqueue = new Queue<GameObject>();
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
    public GameObject getbullet()
    {
        GameObject bullet = bulletqueue.Dequeue();
        bullet.SetActive(true);
        bullet.transform.SetParent(null);
        return bullet;

    }
    public void returnbullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.SetParent(transform);
        bulletqueue.Enqueue(bullet);
    }


    void Start()
    {
        InitialllzePool();

    }
    void InitialllzePool()
    {
        for (int i = 0; i < poolsize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletqueue.Enqueue(bullet);
            bullet.transform.SetParent(transform);
        }
    }
}

