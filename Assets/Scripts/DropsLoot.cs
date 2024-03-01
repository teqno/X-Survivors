using UnityEngine;

public class DropsLoot : MonoBehaviour
{
    public GameObject Loot;

    public void DropLoot()
    {
        Instantiate(Loot, transform);
    }
}
