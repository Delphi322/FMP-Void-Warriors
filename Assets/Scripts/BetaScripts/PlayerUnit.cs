using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public PlayerUnit prefab;
    public static PlayerUnit instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        GameObject sdinky = (GameObject)Resources.Load("Player");
        prefab = sdinky.GetComponent<PlayerUnit>();

        DontDestroyOnLoad(instance);
    }
    private void OnDestroy()
    {
        prefab.unitName = unitName;
        prefab.unitLevel = unitLevel;

        prefab.maxHP = maxHP;
        prefab.maxSP = maxSP;

        prefab.specialDamage = specialDamage;
        prefab.currentHP = prefab.maxHP;
        prefab.currentSP = prefab.maxSP;
    }
}
