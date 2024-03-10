using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public static void UpdateDirection(Weapon weapon, GameObject attackingEntity, Nullable<Vector3> directionPoint = null)
    {
        switch (weapon.type)
        {
            case Weapon.WeaponType.Directional:
                {
                    Vector3 lookDirection;
                    if (directionPoint == null)
                    {
                        // Get direction to mouse on plane
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        new Plane(Vector3.up, Vector3.zero).Raycast(ray, out float enter);
                        lookDirection = ray.GetPoint(enter) - attackingEntity.transform.position;
                        Debug.DrawLine(Camera.main.transform.position, ray.GetPoint(enter));
                    }
                    else
                    {
                        lookDirection = (Vector3)(directionPoint - attackingEntity.transform.position);
                    }
                    // Point collider / projectile
                    weapon.direction = lookDirection.normalized;
                    Debug.DrawRay(attackingEntity.transform.position, weapon.direction, Color.red);
                }
                break;
        }

        MultiCollider collider = weapon.transform.GetComponentInChildren<MultiCollider>();
        if (collider != null)
        {
            collider.transform.right = -weapon.direction;
        }
    }

    public static void ExecuteAttack(Weapon weapon, GameObject attackingEntity, string enemyTag = "Enemy")
    {
        if (weapon.projectile != null)
        {
            for (int i = 0; i < weapon.numOfProjectiles; i++)
            {
                // if projectile - spawn projectile
                GameObject projectileObj = Instantiate(weapon.projectile, attackingEntity.transform.position + Vector3.up * 0.5f, Quaternion.identity, attackingEntity.transform.parent);

                Projectile projectile = projectileObj.GetComponent<Projectile>();
                projectile.direction = weapon.direction;
                projectile.damage = weapon.damage;
                
                if (weapon.type == Weapon.WeaponType.Radial)
                {
                    projectile.direction = Quaternion.Euler(0, 360f / weapon.numOfProjectiles * i, 0) * projectile.direction;
                }
            }
        }

        // if collider - deal damage
        MultiCollider collider = weapon.GetComponentInChildren<MultiCollider>();
        if (collider != null)
        {
            foreach (var col in collider.collisions.FindAll(c => c.tag == enemyTag))
            {
                var attackable = col.gameObject.GetComponent<Attackable>();
                attackable?.TakeDamage(weapon.damage);
            }
        }
    }
}
