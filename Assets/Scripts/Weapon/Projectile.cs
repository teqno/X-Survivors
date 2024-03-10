using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 direction;
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

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        for (int i = 0; i < multiCollider._collisions.Count; i++)
        {
            if (multiCollider._collisions[i] != null)
            {
                var attackable = multiCollider._collisions[i].gameObject.GetComponent<Attackable>();
                attackable?.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
