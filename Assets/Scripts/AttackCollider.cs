using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackCollider : MonoBehaviour
{
    public bool isColliding = false;
    public Collider[] enemies;
    public InputManager playerInput;
    private GameObject player;
    private Enemy enemy;
    private Animator animator;
    private GameObject parent;
    private Attackable attackable;

    private void Start()
    {
        player = GameObject.Find("Player");
        enemy = GetComponentInParent<Enemy>();
        playerInput = GetComponentInParent<InputManager>();
        animator = transform.parent.GetComponentInChildren<Animator>();
        parent = transform.parent.gameObject;
        attackable = parent.GetComponent<Attackable>();
    }

    private void Update()
    {
        if (parent.tag == "Player" && player.GetComponentInChildren<PlayerAttack>().isAttacking ||
            parent.tag == "Enemy" && enemy.isAttacking)
        {
            return;
        }
        Vector2 lookDirection;
        if (transform.parent.tag == "Player")
        {
            Vector2 pointerPos = Mouse.current.position.value;
            lookDirection = pointerPos - new Vector2(Screen.width, Screen.height) * 0.5f;
            transform.right = transform.position - new Vector3(lookDirection.x, 0, lookDirection.y);
        }
        else
        {
            lookDirection = new Vector2(player.transform.position.x, player.transform.position.z);
            transform.right = transform.position - new Vector3(lookDirection.x, 0, lookDirection.y);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!attackable.isState(Attackable.State.None))
        {
            return;
        }
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            isColliding = true;
            if (!enemies.Any(e => e.GetHashCode() == other.GetHashCode()))
            {
                enemies = enemies.Append(other).ToArray();
            }
            if (enemy != null && !enemy.isAttacking && transform.parent.tag == "Enemy" && other?.tag == "Player")
            {
                enemy.isAttacking = true;
                animator.SetTrigger("Attack");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            isColliding = false;
            enemies = enemies.Where(e => e.GetHashCode() != other.GetHashCode()).ToArray();
        }
    }
}
