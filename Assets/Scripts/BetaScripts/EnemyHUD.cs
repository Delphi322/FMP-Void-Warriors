using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{

    public void SetHUD(Unit unit, Text enemyNameText, Text enemyHealthText)
    {
        enemyNameText.text = unit.unitName;
        enemyHealthText.text = unit.currentHP.ToString() + "/" + unit.maxHP;
    }

}
