using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] healthFill;
    public GameObject player;

    void Update()
    {
        for (int i = 0; i < healthFill.Length; i++)
        {
            healthFill[i].fillAmount = player.GetComponent<Attackable>().health / player.GetComponent<Player>().maxHealth;
        }
    }
}
