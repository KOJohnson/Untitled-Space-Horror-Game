using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool Instance;
    public List<GameObject> pooledEnemies;
    public GameObject objectToPool;
    public int poolAmount;
    private GameObject enemyParent;
    
    

    private void Awake()
    {
        Instance = this;
        enemyParent = new GameObject("Enemy Parent");
    }

    private void Start()
    {
        pooledEnemies = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < poolAmount; i++)
        {
            tmp = Instantiate(objectToPool, enemyParent.transform, true);
            tmp.SetActive(false);
            pooledEnemies.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < poolAmount; i++) 
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                return pooledEnemies[i];
                
            }
            
        } return null;
        
    }
}
