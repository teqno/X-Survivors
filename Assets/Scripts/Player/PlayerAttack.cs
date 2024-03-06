using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // public float attackDamage;
    // public float attackSpeed;
    public float attackAnimationSpeed;

    public bool isAttacking = false;
    private InputManager inputManager;
    private Animator animator;
    private MultiCollider attackCollider;
    private AnimationEventHandler eventHandler;
    private Player player;
    private Weapon[] weapons;
    private Attackable attackable;
    private Dictionary<int, float> weaponCooldown;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponentInChildren<Animator>();
        attackCollider = GetComponentInChildren<MultiCollider>();
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
        player = GetComponent<Player>();    
        weapons = GetComponentsInChildren<Weapon>();
        weaponCooldown = weapons.ToDictionary(item => item.GetHashCode(), item => item.cooldown);
        // attackDamage = player.damageMult * weapon.damage;
        // attackSpeed = player.attackSpeedMult * weapon.attackSpeed;
        attackable = GetComponent<Attackable>();
        attackAnimationSpeed = weapons.First().attackSpeed;
    }

    private void OnEnable()
    {
        eventHandler.OnPlayerAttackFinish += ExitAttack;
        eventHandler.OnPlayerAttackDamage += HandleExecuteAttack;
    }

    private void OnDisable()
    {
        eventHandler.OnPlayerAttackFinish -= ExitAttack;
        eventHandler.OnPlayerAttackDamage -= HandleExecuteAttack;
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = attackAnimationSpeed;

        // Auto-attack
        foreach (var weapon in weapons.Where(w => w.triggerType == Weapon.WeaponTriggerType.Auto))
        {
            if (weaponCooldown[weapon.GetHashCode()] <= 0)
            {
                weaponCooldown[weapon.GetHashCode()] = weapon.cooldown;
                WeaponSystem.ExecuteAttack(weapon, gameObject);
            } else
            {
                weaponCooldown[weapon.GetHashCode()] -= Time.deltaTime;
            }
        }

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

    private void HandleExecuteAttack()
    {
        foreach (var weapon in weapons.Where(w => w.triggerType == Weapon.WeaponTriggerType.Manual))
        {
            WeaponSystem.ExecuteAttack(weapon, gameObject);
        }
    }
}
