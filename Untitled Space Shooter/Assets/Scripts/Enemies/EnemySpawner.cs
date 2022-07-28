using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
     public Transform[] spawnPoints;
     public List<Vector3> spawnLocation;

     private float _timeElapsed;

     private void Awake()
     {
          foreach (Transform spawnPoint in spawnPoints)
          {
               var tmp = spawnPoint.transform.position;
               spawnLocation.Add(tmp);
          }
     }

     public void SpawnEnemies(int enemiesToSpawn)
     {
          for (int i = 0; i < enemiesToSpawn; i++)
          {
               GameObject enemy = EnemyObjectPool.Instance.GetPooledObject();
               if (enemy != null)
               {
                    var index = Random.Range(0, spawnLocation.Count);
                    enemy.transform.position = spawnLocation[index];
                    enemy.SetActive(true);
               }
          }

          GameManager.Instance.enemyCount += enemiesToSpawn;
     }

}
