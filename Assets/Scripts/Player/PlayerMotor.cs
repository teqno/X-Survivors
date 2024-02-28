using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Player player;
    private CharacterController controller;
    private Animator animator;
    private SpriteRenderer sprite;
    private Attackable selfAttackble;
    private PlayerAttack selfPlayerAttack;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        selfAttackble = GetComponent<Attackable>();
        selfPlayerAttack = GetComponent<PlayerAttack>();
    }

    // Receive input from InputManager and pass to CharacterController
    public void ProcessMove(Vector2 input)
    {
        if (selfPlayerAttack.isAttacking || !selfAttackble.isState(Attackable.State.None))
        {
            return;
        }

        Vector3 moveDirection = Vector3.zero;
        if (input.magnitude > 0)
        {
            animator.SetInteger("AnimState", 2);
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }

        if (
            input.x > 0 && !sprite.flipX ||
            input.x < 0 && sprite.flipX
            )
        {
            sprite.flipX = !sprite.flipX;
        }
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * player.speed * Time.deltaTime);
    }
}
