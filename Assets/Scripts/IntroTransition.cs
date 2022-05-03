using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTransition : MonoBehaviour
{
    
    public void IntroLevelLoad()
    {
        SceneManager.LoadScene("Debug");
    }

}
