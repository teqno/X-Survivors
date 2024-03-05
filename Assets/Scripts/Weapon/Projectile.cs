using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 direction = new();
    public float damage;

    private MultiCollider multiCollider;

    private void Awake()
    {
        multiCollider = GetComponent<MultiCollider>();
    }

    private void OnEnable()
    {
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed);
    }

    void Update()
    {
        foreach (Collider col in multiCollider.collisions)
        {
            if (col.tag == "Enemy")
            {
                var attackable = col.gameObject.GetComponent<Attackable>();
                attackable?.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
