using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    Unit prefab; 

    public static GameObject Player { get; private set; }

    void Awake()
    {
        player = GameObject.Find("Aigis");
        GameManager.Player = player;

        GameObject sdinky = (GameObject)Resources.Load("PartyMember1");
        prefab = sdinky.GetComponent<Unit>();

        prefab.maxHP = 30;
        prefab.currentHP = prefab.maxHP;


    }
}
