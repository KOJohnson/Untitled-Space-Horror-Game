using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrailPool : MonoBehaviour
{
    public List<TrailRenderer> pooledTrails;
    public TrailRenderer objectToPool;
    public int poolAmount;
    private GameObject _trailParent;
    
    

    private void Awake()
    {
        _trailParent = new GameObject("BulletTrail Parent");
    }

    private void Start()
    {
        pooledTrails = new List<TrailRenderer>();
        TrailRenderer tmp;
        for (int i = 0; i < poolAmount; i++)
        {
            tmp = Instantiate(objectToPool, _trailParent.transform, true);
            tmp.gameObject.SetActive(false);
            pooledTrails.Add(tmp);
        }
    }

    public TrailRenderer GetPooledObject()
    {
        for(int i = 0; i < poolAmount; i++) 
        {
            if (!pooledTrails[i].gameObject.activeInHierarchy)
            {
                return pooledTrails[i];
            }
            
        } return null;
        
    }
}
