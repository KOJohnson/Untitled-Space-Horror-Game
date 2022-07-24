using System;
using Core;
using Core.Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    [SerializeField]private float maxHealth = 100f;
    [SerializeField]private float enemyHealth = 100f;
    
    public float CurrentHealth
    {
        get => enemyHealth;
        set => enemyHealth = value;
    }
    public float EnemyMax => maxHealth;

    private void Awake()
    {
        enemyHealth = maxHealth;
    }

    private void Start()
    {
        GameManager.Instance.AddEnemy();
    }

    private void Update()
    {
        if (!(enemyHealth <= 0)) return;
        enemyHealth = 0;
        Die();
    }


    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
    }

    private void Die()
    {
        GameManager.Instance.RemoveEnemy();
        gameObject.SetActive(false);
    }
}
