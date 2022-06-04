using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthImage;
    public GameObject healthCanvas;
    private Camera _camera;
    
    [SerializeField]private float fillSpeed = 2;
    private float _target = 1;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        healthCanvas.transform.rotation = Quaternion.LookRotation(healthCanvas.transform.position - _camera.transform.position);
        healthImage.fillAmount = Mathf.MoveTowards(healthImage.fillAmount, _target, fillSpeed * Time.deltaTime);
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _target = currentHealth / maxHealth;
    }
}
