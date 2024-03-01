using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public float volume = 1.0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
