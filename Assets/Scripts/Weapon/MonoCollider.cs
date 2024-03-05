using UnityEngine;

public class MonoCollider : MonoBehaviour
{
    public Collider collision;

    private void OnTriggerEnter(Collider other)
    {
        collision = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if (collision == other)
        {
            collision = null;
        }
    }
}
