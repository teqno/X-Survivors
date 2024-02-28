using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemiesParent;
    private float spawnRate = 1f;
    private float timePassd = 0f;
    private float spawnMinDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassd >= spawnRate)
        {
            Vector3 spawnDirection = Quaternion.AngleAxis(Random.value * 360, Vector3.up) * Vector3.right;
            Instantiate(enemyPrefab, transform.position + spawnDirection * spawnMinDistance, Quaternion.identity, enemiesParent.transform);
            timePassd = 0f;
            return;
        } 

        timePassd += Time.deltaTime;
    }
}
