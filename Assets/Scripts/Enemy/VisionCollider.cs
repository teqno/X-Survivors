using UnityEngine;

public class VisionCollider : MonoBehaviour
{
    public Collider player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }
}
