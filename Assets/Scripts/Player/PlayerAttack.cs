using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDamage = 25f;
    public float attackSpeed = 1.0f;

    public bool isAttacking = false;
    private InputManager inputManager;
    private Animator animator;
    private AttackCollider attackCollider;
    private AnimationEventHandler eventHandler;
    private Player player;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponentInChildren<Animator>();
        attackCollider = GetComponentInChildren<AttackCollider>();
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
        player = GetComponent<Player>();    
    }

    private void OnEnable()
    {
        eventHandler.OnPlayerAttackFinish += ExitAttack;
        eventHandler.OnPlayerAttackDamage += DealDamage;
    }

    private void OnDisable()
    {
        eventHandler.OnPlayerAttackFinish -= ExitAttack;
        eventHandler.OnPlayerAttackDamage -= DealDamage;
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = attackSpeed * player.attackSpeedMult;

        if (
            animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            isAttacking = false;
            return;
        }
        if (!isAttacking && inputManager.onFoot.Attack.triggered)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    private void ExitAttack()
    {
        isAttacking = false;
    }

    private void DealDamage()
    {
        attackCollider.enemies = attackCollider.enemies.Where(e => e != null && e.enabled).ToArray();
        foreach (Collider enemy in attackCollider.enemies)
        {
            if (attackCollider.enemies.Any(e => e.GetHashCode() == enemy.GetHashCode()))
            {
                enemy.gameObject.TryGetComponent(out Attackable attackable);
                attackable.TakeDamage(attackDamage * player.damageMult);
            }
        }
    }
}
