using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDecalPool : MonoBehaviour
{
    public static BulletDecalPool instance;
    public List<GameObject> pooledBulletDecals;
    public GameObject objectToPool;
    public int poolAmount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pooledBulletDecals = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < poolAmount; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledBulletDecals.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < poolAmount; i++) 
        {
            if (!pooledBulletDecals[i].activeInHierarchy)
            {
                return pooledBulletDecals[i];
                
            }
            
        } return null;
        
    }
}
