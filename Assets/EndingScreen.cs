using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
   public void EndScreenLoad()
    {
        SceneManager.LoadScene("End Screen");
    }
}
