using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject player;

    public int randomEncounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<OldPlayerController>()) return;
        randomEncounter = Random.Range(0, 10);
        if (randomEncounter == 5)
        {
            player = GameManager.PlayerAE;
            player.SetActive(false);
            SceneManager.LoadScene("Debug Battle", LoadSceneMode.Additive);
        }
        else
            return;
            
    }
}
