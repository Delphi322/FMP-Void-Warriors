using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject enemyPrefab;
    public GameObject[] enemyList;
    public Text[] healthTexts;
    public Text[] nameTexts;
    public Text[] spTexts;
    int randomEnemy;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleState state;

    public BattleHUD playerHUD;
    public EnemyHUD enemyHUD;

    public bool isDefending;

    private Animator heckhook;
    public Animator abilities;

    private SFXManager sfxMan;
    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();
        randomEnemy = Random.Range(0, enemyList.Length);
        enemyPrefab = enemyList[randomEnemy];
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    void Update()
    {
        playerHUD.SetHUD(playerUnit,nameTexts[0],healthTexts[0],spTexts[0]);
        enemyHUD.SetHUD(enemyUnit, nameTexts[1], healthTexts[1]);
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        heckhook = GameObject.Find("AigisBattle_0").GetComponent<Animator>();

        GameObject enemyGO =  Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        yield return new WaitForSeconds(0.00001f);

        state = BattleState.PLAYERTURN;
        playerTurn();

    }

    IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        

        if (isDead)
        {
            state = BattleState.WON;
            yield return new WaitForSeconds(2f);
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator SpecialAttack()
    {
        if (playerUnit.UseSP(5))
        {
            heckhook.SetTrigger("Spell");
            bool isDead = enemyUnit.TakeDamage(playerUnit.specialDamage);
            abilities.SetTrigger("Holy");
            sfxMan.holySpell.Play();
            if (isDead)
            {
                state = BattleState.WON;
                yield return new WaitForSeconds(2f);
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyTurn());
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
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator ItemUse1()
    {
        heckhook.SetTrigger("Item");
        Debug.Log("Item Gaming");
        playerUnit.Heal(15);
        playerUnit.SPRecover(5);
        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());
        
    }

    IEnumerator ItemUse2()
    {
        heckhook.SetTrigger("Item");
        Debug.Log("Item Gaming");
        playerUnit.Heal(10);
        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn());

    }

    IEnumerator PlayerDefend()
    {
        isDefending = true;
        heckhook.SetTrigger("Defend");
        state = BattleState.ENEMYTURN;
        yield return new WaitForSeconds(2f);
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
                isDead = SpecialAttack(enemyUnit.specialDamage);
                break;
        }
        
        
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            yield return new WaitForSeconds(2f);
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
        if(state == BattleState.WON)
        {
            System.Threading.Thread.Sleep(2000);
            Debug.Log("u da winer");
            playerUnit.unitLevel += 1;
            playerPrefab = GameManager.PlayerAE;
            playerPrefab.SetActive(true);
            SceneManager.UnloadSceneAsync("Debug Battle");
            MusicPlayer.PlayMenuMusic();
        }
        else if(state == BattleState.LOST)
        {
            Debug.Log("You smelly loser");
            System.Threading.Thread.Sleep(2000);
            Application.Quit();
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
