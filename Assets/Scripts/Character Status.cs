using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthStatusData", menuName = "StatusObjects/Health", order = 1)]
public class CharacterStatus : ScriptableObject
{

    public string unitName = "name";
    public int unitLevel;
    public int maxHP = 30;
    public int currentHP = 30;
    public int maxSP = 20;
    public int currentSP = 20;
    public float[] position = new float[2];
    public GameObject characterGameObject;

}
