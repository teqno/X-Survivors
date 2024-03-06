using UnityEngine;

public class GameState : MonoBehaviour
{
    public static int score = 0;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
}
