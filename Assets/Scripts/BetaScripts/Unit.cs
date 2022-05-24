using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int maxSP;
    public int currentSP;
    public int specialDamage;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool UseSP(int cost)
    {

        if (currentSP < cost)
        {
            Debug.Log("Not enough SP");
            return false;
        }
        currentSP -= cost;
        return true;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void SPRecover(int amount)
    {
        currentSP += amount;
        if (currentSP > maxSP)
            currentSP = maxSP;
    }
}
