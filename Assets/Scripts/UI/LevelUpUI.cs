using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    public Player player;
    private BuffCardState[] buffCardStates;
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
        ApplyBuffs();
    }

    private void ApplyBuffs()
    {
        buffCardStates = GetComponentsInChildren<BuffCardState>();
        foreach (BuffCardState state in buffCardStates.Where(s => s.isSelected))
        {
            player.damageMult += state.damageMult;
            player.speed *= state.moveSpeedMult;
            player.maxHealth *= state.maxHealthMult;
        }
    }
}
