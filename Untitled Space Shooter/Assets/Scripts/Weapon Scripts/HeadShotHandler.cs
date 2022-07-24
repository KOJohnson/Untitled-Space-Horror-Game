using Core.Interfaces;
using UnityEngine;

public class HeadShotHandler : MonoBehaviour, IDamageable
{
    private Enemy enemy;
    private HealthBar healthBar;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        healthBar = GetComponentInParent<HealthBar>();
    }

    public void TakeDamage(float damage)
    {
        enemy.CurrentHealth -= damage;
        healthBar.UpdateHealthBar(enemy.EnemyMax, enemy.CurrentHealth);

        if (enemy.CurrentHealth <= 0)
        {
            HeadPop();
        }
    }
    
    private void HeadPop()
    {
        gameObject.SetActive(false);
    }
    
}
