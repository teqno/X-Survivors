using System;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnPlayerAttackFinish;

    public event Action OnEnemyAttackFinish;

    public event Action OnHurtFinish;

    public event Action OnPlayerAttackDamage;

    public event Action OnEnemyAttackDamage;

    private void PlayerAttackAnimationFinishedTrigger() => OnPlayerAttackFinish?.Invoke();
    
    private void EnemyAttackAnimationFinishedTrigger() => OnEnemyAttackFinish?.Invoke();

    private void HurtAnimationFinishedTrigger() => OnHurtFinish?.Invoke(); 

    private void PlayerAttackDamageAnimationTrigger() => OnPlayerAttackDamage?.Invoke();

    private void EnemyAttackDamageAnimationTrigger() => OnEnemyAttackDamage?.Invoke();
}
