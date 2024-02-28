using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackable : Attackable
{
    private float currentDamageAmount = 0f;
    private AnimationEventHandler eventHandler;
    private Animator animator;
    private DropsLoot dropsLoot;

    private void Awake()
    {
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
        animator = GetComponentInChildren<Animator>();
        dropsLoot = GetComponent<DropsLoot>();
    }

    private void OnEnable()
    {
        eventHandler.OnHurtFinish += ExitHurt;
        OnDeath += EnterDeath;
    }

    private void OnDisable()
    {
        eventHandler.OnHurtFinish -= ExitHurt; 
        OnDeath -= EnterDeath;
    }
    
    public override void TakeDamage(float damageAmount)
    {
        if (!isState(State.None)) return;
        SetHurt();
        currentDamageAmount = damageAmount;
        animator.SetTrigger("Hurt");
    }
    
    private void ExitHurt()
    {
        base.TakeDamage(currentDamageAmount);
        currentDamageAmount = 0f;
    }

    private void EnterDeath()
    {
        animator.SetTrigger("Death");
        GetComponent<CapsuleCollider>().enabled = false;
        dropsLoot?.DropLoot();
        Destroy(gameObject, 5f);
    }
}
