using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public bool isAttacking = false;

    private Animator animator;
    private VisionCollider visionCollider;
    private SpriteRenderer sprite;
    private Attackable selfAttackable;
    private MultiCollider attackCollider;
    private AnimationEventHandler eventHandler;
    private Weapon[] weapons;

    // Start is called before the first frame update
    void Awake()
    {
        visionCollider = GetComponentInChildren<VisionCollider>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        selfAttackable = GetComponent<Attackable>();
        attackCollider = GetComponentInChildren<MultiCollider>();
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
        weapons = GetComponentsInChildren<Weapon>();
    }

    private void OnEnable()
    {
        eventHandler.OnEnemyAttackFinish += ExitAttack;
        eventHandler.OnEnemyAttackDamage += HandleExecuteAttack;
    }

    private void OnDisable()
    {
        eventHandler.OnEnemyAttackFinish -= ExitAttack;
        eventHandler.OnEnemyAttackDamage -= HandleExecuteAttack;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!selfAttackable.isState(Attackable.State.None))
        {
            isAttacking = false;
            return;
        }

        if (isAttacking)
        {
            return;
        }
        if (visionCollider.player != null)
        {
            Vector3 distance = visionCollider.player.transform.position - gameObject.transform.position;
            if (distance.magnitude < 0.5f)
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
           
            foreach (var weapon in weapons.Where(w => w.type == Weapon.WeaponType.Directional))
            {
                WeaponSystem.UpdateDirection(weapon, gameObject, visionCollider.player.transform.position);
            } 

            if (!isAttacking && attackCollider.collisions.Exists(e => e.tag == "Player"))
            {
                StartAttack();
            }
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }
    }
    
    private void StartAttack()
    {
        Debug.Log("Attack started");
        isAttacking = true;
        animator.SetTrigger("Attack");
    }

    private void ExitAttack()
    {
        isAttacking = false;
    }

    private void HandleExecuteAttack()
    {
        foreach (var weapon in weapons)
        {
            WeaponSystem.ExecuteAttack(weapon, gameObject, "Player");
        }
    }
}
