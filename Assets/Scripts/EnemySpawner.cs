using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public int randomEncounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        randomEncounter = Random.Range(0, 10);
        if (randomEncounter == 5)
            SceneManager.LoadScene("Debug Battle");
        else
            return;
            
    }
}
