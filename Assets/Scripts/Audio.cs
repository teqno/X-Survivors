using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource m_Source;

    private void Awake()
    {
        m_Source = GetComponent<AudioSource>();
    }

    private void OnGUI()
    {
        if (MainManager.Instance != null && m_Source.volume != MainManager.Instance.volume)

        {
            m_Source.volume = MainManager.Instance.volume;
        }
    }
}
