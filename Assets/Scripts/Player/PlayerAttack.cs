using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDamage;
    public float attackSpeed;

    public bool isAttacking = false;
    private InputManager inputManager;
    private Animator animator;
    private AttackCollider attackCollider;
    private AnimationEventHandler eventHandler;
    private Player player;
    private Weapon weapon;
    private Attackable attackable;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponentInChildren<Animator>();
        attackCollider = GetComponentInChildren<AttackCollider>();
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
        player = GetComponent<Player>();    
        weapon = GetComponentInChildren<Weapon>();
        attackDamage = player.damageMult * weapon.damage;
        attackSpeed = player.attackSpeedMult * weapon.attackSpeed;
        attackable = GetComponent<Attackable>();
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
        animator.speed = attackSpeed;

        if (!attackable.isState(Attackable.State.None))
        {
            isAttacking = false;
            return;
        }
        if (!isAttacking && inputManager.onFoot.Attack.triggered)
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
    }

    private void ExitAttack()
    {
        isAttacking = false;
    }

    private void DealDamage()
    {
        foreach (var collider in attackCollider.collisions.FindAll(c => c.tag == "Enemy")) 
        {
            var attackable = collider.gameObject.GetComponent<Attackable>();
            attackable?.TakeDamage(attackDamage);
        }
    }
}
