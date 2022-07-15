using System;
using Core.Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    public HealthBar healthBar;
    [SerializeField]private float maxHealth = 100f;
    [SerializeField]private float enemyHealth = 100f;

    public bool isDead;
    public float currentHealth
    {
        get => enemyHealth;
        set => enemyHealth = value;
    }
    public float EnemyMax => maxHealth;

    private void Awake()
    {
        enemyHealth = maxHealth;
        //healthBar.UpdateHealthBar(maxHealth, enemyHealth);
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
        //healthBar.UpdateHealthBar(maxHealth, enemyHealth);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
