using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class WaveAction
{
    public string name;
    public float delay;
    public UnityEvent myEvent;
    public GameObject prefab;
    public int spawnCount;
    public string message;
}
 
[System.Serializable]
public class Wave
{
    public string name;
    public List<WaveAction> actions;
}
public class WaveSpawner : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    private IEnumerator _spawnLoop;
    public float difficultyFactor = 0.9f;
    public int currentSpawnWave;
    public List<Wave> waves;
    private Wave m_CurrentWave;
    public Wave CurrentWave { get {return m_CurrentWave;} }
    private float m_DelayFactor = 1.0f;
 
    private IEnumerator SpawnLoop()
    {
        m_DelayFactor = 1.0f;
        while(true)
        {
            foreach(Wave W in waves)
            {
                m_CurrentWave = W;
                foreach(WaveAction A in W.actions)
                {
                    if(A.delay > 0)
                        yield return new WaitForSeconds(A.delay * m_DelayFactor);
                    if (A.message != "")
                    {
                        // A.prefab != null &&
                        // TODO: print ingame message
                    }
                    
                    A.myEvent?.Invoke();
                    //enemySpawner.SpawnEnemies(A.spawnCount);
                    currentSpawnWave++;
                    
                }
                yield return null;  // prevents crash if all delays are 0
            }
            m_DelayFactor *= difficultyFactor;
            yield return null;  // prevents crash if all delays are 0
        }
        
    }

    public void BeginWaves()
    {
        StartCoroutine(_spawnLoop);
    }

    private void Awake()
    {
        _spawnLoop = SpawnLoop();
    }

    private void Update()
    {
        if (currentSpawnWave >= waves.Count)
        {
            StopCoroutine(_spawnLoop);
        }
    }
    
}
