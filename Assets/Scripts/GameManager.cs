using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    PlayerUnit prefab; 

    public static GameObject PlayerAE { get; private set; }

    void Awake()
    {
        player = GameObject.Find("Aigis");
        GameManager.PlayerAE = player;

        GameObject sdinky = (GameObject)Resources.Load("Player");
        prefab = sdinky.GetComponent<PlayerUnit>();

        prefab.maxHP = 30;
        prefab.currentHP = prefab.maxHP;
        prefab.damage = 7;
        prefab.maxSP = 20;
        prefab.currentSP = prefab.maxSP;

    }
}
