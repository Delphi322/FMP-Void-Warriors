using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{

    public Text enemyText;
    public Slider enemyhpSlider;

    public void SetHUD(Unit unit)
    {
        enemyText.text = unit.unitName;
        enemyhpSlider.maxValue = unit.maxHP;
        enemyhpSlider.value = unit.currentHP;
    }


    public void SetHP(int hp)
    {
        enemyhpSlider.value = hp;
    }
}
