using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public AudioSource holySpell;
    public AudioSource healSpell;

    private static bool sfxmanExists;

    // Start is called before the first frame update
    void Start()
    {
        if (!sfxmanExists)
        {
            sfxmanExists = true;
        }
    }
}
