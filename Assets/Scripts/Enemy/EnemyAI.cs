using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public bool isAttacking = false;

    private Animator animator;
    private VisionCollider visionCollider;
    private SpriteRenderer sprite;
    private Attackable selfAttackable;
    private AttackCollider attackCollider;
    private AnimationEventHandler eventHandler;
    private Weapon weapon;

    // Start is called before the first frame update
    void Awake()
    {
        visionCollider = GetComponentInChildren<VisionCollider>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        selfAttackable = GetComponent<Attackable>();
        attackCollider = GetComponentInChildren<AttackCollider>();
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
        weapon = GetComponentInChildren<Weapon>();
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
            Debug.Log("inside ;" + isAttacking);
            Vector3 distance = visionCollider.player.transform.position - gameObject.transform.position;
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
            Vector2 lookDirection = new Vector2(visionCollider.player.transform.position.x, visionCollider.player.transform.position.z);
            attackCollider.transform.right = attackCollider.transform.position - new Vector3(lookDirection.x, 0, lookDirection.y);

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

    private void DealDamage()
    {
        foreach (Collider collider in attackCollider.collisions.FindAll(e => e.tag == "Player"))
        {
            var attackable = collider.gameObject.GetComponent<Attackable>();
            attackable?.TakeDamage(weapon.damage);
        }
    }
}
