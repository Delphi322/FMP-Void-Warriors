using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0f;

    public GameObject player;

    public int randomEncounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<OldPlayerController>()) return;
        randomEncounter = Random.Range(0, 10);
        if (randomEncounter == 5)
        {
            StartCoroutine(LoadLevel());

            player = GameManager.PlayerAE;
            player.SetActive(false);
            
            SceneManager.LoadScene("Debug Battle", LoadSceneMode.Additive);
        }
        else
            return;
            
    }
    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(transitionTime);

        transition.SetTrigger("Start");
    }
}
