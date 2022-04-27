using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    //public Animator transition;
    //public float transitionTime = 10f;
    public GameObject other;

    public GameObject player;
    public SceneTransition transgender;

    public int randomEncounter;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<OldPlayerController>()) return;
        randomEncounter = Random.Range(0, 10);
        if (randomEncounter == 5)
        {
            transgender.LoadLevel();
            other.GetComponent<SceneTransition>();
            //StartCoroutine(LoadLevel());

            player = GameManager.PlayerAE;
            player.SetActive(false);
            
            //SceneManager.LoadScene("Debug Battle", LoadSceneMode.Additive);
        }
        else
            return;
            
    }
   // IEnumerator LoadLevel()
  //  {
   //     yield return new WaitForSeconds(transitionTime);
//
   //     transition.SetTrigger("Start");
   // }
}
