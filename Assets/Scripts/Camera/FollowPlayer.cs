using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 20, -20);    
    }
}
