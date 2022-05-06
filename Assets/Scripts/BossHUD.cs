using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHUD : MonoBehaviour
{
    public void SetHUD(Unit unit, Text bossNameText, Text bossHealthText)
    {
        bossNameText.text = unit.unitName;
        bossHealthText.text = unit.currentHP.ToString() + "/" + unit.maxHP;
    }
}
