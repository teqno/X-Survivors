using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : MonoBehaviour
{
    public int value = 10;

    private Collider other;
    private bool isColliding = false;
    private float speed = 1.0f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isColliding = true;
            this.other = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isColliding = false;
            this.other = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding)
        {
            Vector3 distance = other.transform.position - transform.position;
            if (distance.magnitude < 0.5f)
            {
                other.GetComponent<Player>().GainXP(value);
                Destroy(gameObject);
            }
            Vector3 direction = Vector3.Normalize(distance);
            Vector3 translation = direction * speed * Time.deltaTime;
            speed *= 1f + Time.deltaTime;
            transform.Translate(translation);
        }
    }
}
