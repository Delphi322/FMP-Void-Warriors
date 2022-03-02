using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Button booton;

    private void Start()
    {
        booton.Select();

    }

    public void SetHUD(Unit unit, Text nameText, Text healthText)
    {
        nameText.text = unit.unitName;
        healthText.text = unit.currentHP.ToString() + "/" + unit.maxHP;
    }
}
