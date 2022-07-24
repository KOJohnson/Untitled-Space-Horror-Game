using System;
using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    public float rotationSpeed;
    public float updateSpeed = 0.1f; // how often to recalculate the path to target position

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private void Update()
    {
        var direction = (GameManager.Instance.player.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
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
