using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Button booton;

    public void Start()
    {
        booton.Select();

    }

    public void SetHUD(Unit unit, Text nameText, Text healthText)
    {
        nameText.text = unit.unitName;
        healthText.text = unit.currentHP.ToString() + "/" + unit.maxHP;
    }
    public void SelectButton(Button button)
    {
        button.Select();
    }
}
