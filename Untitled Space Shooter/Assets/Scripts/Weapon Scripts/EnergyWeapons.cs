using UnityEngine;

[CreateAssetMenu(fileName = "EnergyWeapon", menuName = "ScriptableObjects/EnergyWeapons", order = 0)]
public class EnergyWeapons : ScriptableObject
{
    public float weaponDamage;
    public float headshotMultiplier;
    public float fireRate;
    public float maxDistance;
    
    [Tooltip("How fast weapon overheats")]
    public float overheatSpeed;
    [Tooltip("Value before weapon overheats")]
    public int maxHeatCapacity;
    [Tooltip("Keep at 0")]
    public int minHeatCapacity;
    [Tooltip("Minimum value needed before weapon can fire again after overheating")]
    public int minHeatThreshold;

    public AudioClip fireSound;
    public TrailRenderer bulletTracer;
}
