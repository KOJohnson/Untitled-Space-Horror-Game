using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    public static EnemyBulletPool instance;
    public List<GameObject> pooledBullets;
    public GameObject objectToPool;
    public int poolAmount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pooledBullets = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < poolAmount; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledBullets.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < poolAmount; i++) 
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
            
        } return null;
        
    }
}


