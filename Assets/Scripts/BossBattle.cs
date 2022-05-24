using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BossState { START, PLAYERTURN, BOSSTURN, WON, LOST }

public class BossBattle : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject bossPrefab;
    public Text[] healthTexts;
    public Text[] nameTexts;
    public Text[] spTexts;

    public Transform playerBattleStation;
    public Transform bossBattleStation;

    Unit playerUnit;
    Unit bossUnit;

    public BossState state;

    public BattleHUD playerHUD;
    public BossHUD bossHUD;

    public bool isDefending;

    private Animator heckhook;
    public Animator abilities;

    private SFXManager sfxMan;

    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();
        state = BossState.START;
        StartCoroutine(SetupBattle());
    }

    void Update()
    {
        playerHUD.SetHUD(playerUnit,nameTexts[0],healthTexts[0],spTexts[0]);
        bossHUD.SetHUD(bossUnit, nameTexts[1], healthTexts[1]);
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        heckhook = GameObject.Find("AigisBattle_0").GetComponent<Animator>();

        GameObject bossGO =  Instantiate(bossPrefab, bossBattleStation);
        bossUnit = bossGO.GetComponent<Unit>();

        yield return new WaitForSeconds(0.000001f);

        state = BossState.PLAYERTURN;
        playerTurn();

    }

    IEnumerator PlayerAttack()
    {

        bool isDead = bossUnit.TakeDamage(playerUnit.damage);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BossState.WON;
            EndBattle();
        }
        else
        {
            state = BossState.BOSSTURN;
            StartCoroutine(BossTurn());
        }
    }

    IEnumerator SpecialAttack()
    {
        if (playerUnit.UseSP(5))
        {
            heckhook.SetTrigger("Spell");
            bool isDead = bossUnit.TakeDamage(playerUnit.specialDamage);
            abilities.SetTrigger("Holy");
            sfxMan.holySpell.Play();
            yield return new WaitForSeconds(2f);
            if (isDead)
            {
                state = BossState.WON;

                EndBattle();
            }
            else
            {
                state = BossState.BOSSTURN;
                StartCoroutine(BossTurn());
            }
        }
    }

    IEnumerator HealSpell()
    {
        if (playerUnit.UseSP(3))
        {
            heckhook.SetTrigger("Spell");
            playerUnit.Heal(20);
            abilities.SetTrigger("Heal");
            sfxMan.healSpell.Play();
            yield return new WaitForSeconds(2f);
            state = BossState.BOSSTURN;
            StartCoroutine(BossTurn());
        }
    }

    IEnumerator ItemUse1()
    {
        heckhook.SetTrigger("Item");
        Debug.Log("Item Gaming");
        playerUnit.Heal(15);
        playerUnit.SPRecover(5);
        yield return new WaitForSeconds(2f);
        state = BossState.BOSSTURN;
        StartCoroutine(BossTurn());
        
    }

    IEnumerator ItemUse2()
    {
        heckhook.SetTrigger("Item");
        Debug.Log("Item Gaming");
        playerUnit.Heal(10);
        yield return new WaitForSeconds(2f);
        state = BossState.BOSSTURN;
        StartCoroutine(BossTurn());

    }

    IEnumerator PlayerDefend()
    {
        isDefending = true;
        heckhook.SetTrigger("Defend");
        yield return new WaitForSeconds(2f);
        state = BossState.BOSSTURN;
        StartCoroutine(BossTurn());
    }

    IEnumerator BossTurn()
    {
        bool isDead = false;
        int choice = Random.Range(0, 2);
        switch (choice)
        {
            case 0:
                isDead = Attack(bossUnit.damage);
                break;
            case 1:
                isDead = SpecialAttack(bossUnit.specialDamage);
                break;
        }
        
        
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BossState.LOST;
            EndBattle();
        }
        else
        {
            state = BossState.PLAYERTURN;
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
    
    bool SpecialAttack(int specialDamage)
    {
        bool isDead = false;
        if (isDefending) isDead = playerUnit.TakeDamage(specialDamage / 2);
        else isDead = playerUnit.TakeDamage(specialDamage);
        return isDead;

    }

    void EndBattle()
    {
        if(state == BossState.WON)
        {
            Debug.Log("u da winer");
            playerUnit.unitLevel += 1;
            playerPrefab = GameManager.PlayerAE;
            playerPrefab.SetActive(true);
            SceneManager.LoadScene("Ending Cutscene");
        }
        else if(state == BossState.LOST)
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
        if (state == BossState.PLAYERTURN)
            StartCoroutine(PlayerAttack());
        else
            return;

    }
    public void onSpecialButton()
    {
        if (state == BossState.PLAYERTURN)
            StartCoroutine(SpecialAttack());
        else
            return;
    }

    public void onHealSpell()
    {
        if (state == BossState.PLAYERTURN)
            StartCoroutine(HealSpell());
        else
            return;
    }

    public void onItemButton1()
    {

        if (state == BossState.PLAYERTURN)
            StartCoroutine(ItemUse1());
        else
            return;
    }

    public void onItemButton2()
    {

        if (state == BossState.PLAYERTURN)
            StartCoroutine(ItemUse2());
        else
            return;
    }

    public void onDefendButton()
    {
        if (state == BossState.PLAYERTURN)
            StartCoroutine(PlayerDefend());
        else
            return;
    }
}
