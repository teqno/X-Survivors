using System.Linq;
using UnityEngine;

public class EnemyAttackable : Attackable
{
    private float currentDamageAmount = 0f;
    private AnimationEventHandler eventHandler;
    private Animator animator;
    private DropsLoot dropsLoot;
    private GameObject gameOverUI;

    private void Awake()
    {
        eventHandler = GetComponentInChildren<AnimationEventHandler>();
        animator = GetComponentInChildren<Animator>();
        dropsLoot = GetComponent<DropsLoot>();
        gameOverUI = Resources.FindObjectsOfTypeAll<GameObject>().First(e => e.name == "GameOverUI");
    }

    private void OnEnable()
    {
        eventHandler.OnHurtFinish += ExitHurt;
        OnDeath += EnterDeath;

        if (gameObject.tag == "Player")
        {
            eventHandler.OnPlayerDeathFinish += ExitPlayerDeath;
        }
    }

    private void OnDisable()
    {
        eventHandler.OnHurtFinish -= ExitHurt; 
        OnDeath -= EnterDeath;
        eventHandler.OnPlayerDeathFinish -= ExitPlayerDeath;
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
        if (gameObject.tag == "Enemy")
        {
            GameState.score += 1;
            GetComponent<CapsuleCollider>().enabled = false;
        }

        animator.SetTrigger("Death");
        dropsLoot?.DropLoot();
        Destroy(gameObject, 5f);
    }

    private void ExitPlayerDeath()
    {
        gameOverUI.SetActive(true);
    }
}
