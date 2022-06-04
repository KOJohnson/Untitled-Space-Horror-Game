using System;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
     public bool spawnEnemies;
     public int enemyCount;


     private void Update()
     {
          if (spawnEnemies)
          {
               GameObject enemy = EnemyObjectPool.instance.GetPooledObject();
               if (enemy != null)
               {
                    enemy.SetActive(true);
               }
          }
     }
}
