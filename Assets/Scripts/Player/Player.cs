using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 200.0f;
    public float speed = 5.0f;
    public float damageMult = 1f;
    public float attackSpeedMult = 1f;
    public int maxXp = 100;
    public int xp = 0;
    public int level = 1;

    public void GainXP(int xp)
    {
        this.xp += xp;
        if (this.xp >= maxXp)
        {
            GainLevel();
        }
    }

    public void GainLevel()
    {
        level++;
        xp -= maxXp;
        maxXp += 50;
        attackSpeedMult = 1f + level * 0.5f;
        GameObject levelUpUI = Resources.FindObjectsOfTypeAll<GameObject>().First(e => e.name == "LevelUpUI");
        levelUpUI.SetActive(true);
    }
}

