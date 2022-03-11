using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public Text[] healthTexts;
    public Text[] nameTexts;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleState state;

    public BattleHUD playerHUD;
    public EnemyHUD enemyHUD;

    public bool isDefending;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    void Update()
    {
        playerHUD.SetHUD(playerUnit,nameTexts[0],healthTexts[0]);
        enemyHUD.SetHUD(enemyUnit, nameTexts[1], healthTexts[1]);
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO =  Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        yield return new WaitForSeconds(0.000001f);

        state = BattleState.PLAYERTURN;
        playerTurn();

    }

    IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator SpecialAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.specialDamage);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator HealSpell()
    {
        playerUnit.Heal(5);

        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator ItemUse1()
    {
        Debug.Log("Item Gaming");
        playerUnit.Heal(5);
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        
    }

    IEnumerator ItemUse2()
    {
        Debug.Log("Item Gaming");
        playerUnit.Heal(2);
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    IEnumerator PlayerDefend()
    {
        isDefending = true;

        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        bool isDead = false;
        int choice = Random.Range(0, 2);
        switch (choice)
        {
            case 0:
                isDead = Attack(enemyUnit.damage);
                break;
            case 1:
                break;
        }
        
        
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            playerTurn();
        }
    }
    bool Attack(int damage)
    {
        bool isDead = false;
        if (isDefending) isDead = playerUnit.TakeDamage(damage / 2);
        else isDead = playerUnit.TakeDamage(damage);
        return isDead;
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            Debug.Log("u da winer");
            playerUnit.unitLevel += 1;
        }else if(state == BattleState.LOST)
        {
            Debug.Log("You smelly loser");
        }
    }
    
    void playerTurn()
    {
        playerHUD.Start();
    }

    public void onAttackButton()
    {
        if (state == BattleState.PLAYERTURN)
            StartCoroutine(PlayerAttack());
        else
            return;

    }
    public void onSpecialButton()
    {
        if (state == BattleState.PLAYERTURN)
            StartCoroutine(SpecialAttack());
        else
            return;
    }

    public void onHealSpell()
    {
        if (state == BattleState.PLAYERTURN)
            StartCoroutine(HealSpell());
        else
            return;
    }

    public void onItemButton1()
    {

        if (state == BattleState.PLAYERTURN)
            StartCoroutine(ItemUse1());
        else
            return;
    }

    public void onItemButton2()
    {

        if (state == BattleState.PLAYERTURN)
            StartCoroutine(ItemUse2());
        else
            return;
    }

    public void onDefendButton()
    {
        if (state == BattleState.PLAYERTURN)
            StartCoroutine(PlayerDefend());
        else
            return;
    }
}
