using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounter : MonoBehaviour
{
    public GameObject other;

    public GameObject player;
    public SceneTransition transgender;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<OldPlayerController>()) return;
        
            transgender.LoadLevel();
            other.GetComponent<SceneTransition>();

            player = GameManager.PlayerAE;
            player.SetActive(false);


    }
}
