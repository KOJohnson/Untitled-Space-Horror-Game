using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnTrigger : MonoBehaviour
{
    public UnityEvent spawnEnemy;
    public Collider triggerCollider;
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
            triggerCollider.enabled = false;
        }
    }
    
}
