using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;

    private Animator animator;
    private VisionCollider visionCollider;
    private SpriteRenderer sprite;
    private Enemy selfEnemy;
    private Attackable selfAttackable;

    // Start is called before the first frame update
    void Start()
    {
        visionCollider = GetComponentInChildren<VisionCollider>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        selfEnemy = GetComponent<Enemy>();
        selfAttackable = GetComponent<Attackable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selfEnemy.isAttacking || !selfAttackable.isState(Attackable.State.None))
        {
            return;
        }
        if (visionCollider.player != null)
        {
            Vector3 distance = visionCollider.player.transform.position - gameObject.transform.position;
            Debug.Log(distance.magnitude);
            if (distance.magnitude < 1f)
            {
                animator.SetInteger("AnimState", 0);
                return;
            }
            animator.SetInteger("AnimState", 2);
            Vector3 direction = Vector3.Normalize(distance);
            Vector3 translation = direction * speed * Time.deltaTime;
            gameObject.transform.Translate(translation);

            if (
                translation.x > 0 && !sprite.flipX ||
                translation.x < 0 && sprite.flipX
                )
            {
                sprite.flipX = !sprite.flipX;
            }
        } else
        {
            animator.SetInteger("AnimState", 0);
        }
    }
}
