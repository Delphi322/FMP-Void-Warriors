using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;

    public void Loadnextlevel()
    {
        SceneManager.LoadScene("Debug Battle", LoadSceneMode.Additive);
        MusicPlayer.PlayGameMusic();
    }

    public void LoadLevel()
    {
        transition.SetTrigger("Start");
    }
}
