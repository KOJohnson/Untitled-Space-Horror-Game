using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    public float updateSpeed = 0.1f; // how often to recalculate the path to target position

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowTarget());
    }

    IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);

        while (enabled)
        {
            _agent.destination = GameManager.Instance.player.position;

            yield return wait;
        }
    }
}
