using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackDamage = 25f;

    public bool isAttacking = false;
    private Animator animator;
    private AttackCollider attackCollider;
    private AnimationEventHandler eventHandler;
    private EnemyAttackable selfAttackable;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        attackCollider = GetComponentInChildren<AttackCollider>();
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
        selfAttackable = GetComponent<EnemyAttackable>();
    }

    private void OnEnable()
    {
        eventHandler.OnEnemyAttackFinish += ExitAttack;
        eventHandler.OnEnemyAttackDamage += DealDamage;
    }

    private void OnDisable()
    {
        eventHandler.OnEnemyAttackFinish -= ExitAttack;
        eventHandler.OnEnemyAttackDamage -= DealDamage;
    }

    private void Update()
    {
        if (!selfAttackable.isState(Attackable.State.None)) {
            isAttacking = false;
            return;
        }

        // if (!isAttacking && attackCollider.isColliding && attackCollider.enemies.Any(e => e.tag == "Player"))
        // {
        //     isAttacking = true;
        //     animator.SetTrigger("Attack");
        // }
    }

    private void ExitAttack()
    {
        isAttacking = false;
    }

    private void DealDamage()
    {
        attackCollider.enemies = attackCollider.enemies.Where(e => e != null && e.enabled).ToArray();
        foreach (Collider enemy in attackCollider.enemies.Where(e => e.tag == "Player"))
        {
            enemy.gameObject.TryGetComponent(out Attackable attackable);
            if (attackCollider.enemies.Any(e => e.GetHashCode() == enemy.GetHashCode()))
            {
                attackable.TakeDamage(attackDamage);
            }
        }
    }
}
