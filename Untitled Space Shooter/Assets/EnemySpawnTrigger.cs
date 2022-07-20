using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnTrigger : MonoBehaviour
{
    public UnityEvent spawnEnemy;
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            spawnEnemy?.Invoke();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
    
}
