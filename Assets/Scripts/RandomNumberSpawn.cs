using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumberSpawn : MonoBehaviour
{

    int randomEnemySpawn;
    private void Start()
    {
        randomEnemySpawn = Random.Range(0, 10);
    }

    private void Update()
    {
        
    }
}
