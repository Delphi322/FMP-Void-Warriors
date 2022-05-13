using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTransition : MonoBehaviour
{
    public Animator transitionandon;
    public float transitionTime = 2f;

    public void Loadnextlevel()
    {
        SceneManager.LoadScene("Debug Boss");
    }

    public void LoadLevel()
    {
        transitionandon.SetTrigger("Start");
    }
}
