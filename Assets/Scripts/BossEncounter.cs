using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEncounter : MonoBehaviour
{
    public GameObject other;
    public Animator transitionandon;
    public GameObject player;
    public SceneTransition Transitioning;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<OldPlayerController>()) return;

        Transitioning.LoadLevel();
        SceneManager.LoadScene("Debug Boss");

    }

    public void LoadLevel()
    {
        transitionandon.SetTrigger("Start");
    }
}
