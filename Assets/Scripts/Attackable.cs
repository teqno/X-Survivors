using System;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    public float health;

    public enum State 
    {
        Dead,
        Hurt,
        None
    }

    public State state = State.None;

    public event Action OnDeath;

    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        health = player ? player.maxHealth : 100f;
    }

    public virtual void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            SetDead();
            OnDeath.Invoke();
            return;
        }
        SetNone();
    }

    public bool isState(State state)
    {
        return state == this.state;
    }

    protected void SetDead()
    {
        state = State.Dead;
    }

    protected void SetHurt()
    {
        state = State.Hurt;
    }

    protected void SetNone()
    {
        state = State.None;
    }
}
