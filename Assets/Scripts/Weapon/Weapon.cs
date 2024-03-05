using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponTriggerType
    {
        Manual,
        Auto
    }

    public enum WeaponType
    {
        Directional,
        Radial
    }
    
    public WeaponTriggerType triggerType = WeaponTriggerType.Manual;
    public WeaponType type = WeaponType.Directional;
    public float damage = 100f;
    public float attackSpeed = 1f;
    public float cooldown;
    public GameObject projectile;
    public int numOfProjectiles = 1;
    public Vector3 direction = Vector3.zero;

    private void Awake()
    {
        cooldown = 1 / attackSpeed;
    }
}
