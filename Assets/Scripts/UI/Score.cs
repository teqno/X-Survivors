using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        Debug.Log(text?.text);
        text.text = GameState.score.ToString(); 
    }
}
