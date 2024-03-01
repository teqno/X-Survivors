using UnityEngine;
using UnityEngine.UI;

public class BuffCardState : MonoBehaviour
{
    public float damageMult = 1f;
    public float maxHealthMult = 1f;
    public float moveSpeedMult = 1f;

    public bool isSelected = false;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Select);
    }

    public void Select()
    {
        isSelected = true;
    }
}
