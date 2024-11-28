using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }//It's a state machine(Or atleast I think that's what it's called.) 
//Look at brakeys turn based combat tutorial to learn more about it.

/*
 NOTE!!!!!!! 1/10/24
For anyone who is working on this, I added comments to explain some of it. The code is pretty messy and might need
a bit of rewriting down the line but for now it works. It's mostly a frankenstine of different tutorials, the biggest of
which being Brakey's turn based combat tutorial. The biggest challenge with that tutorial was making it work with multiple 
party members, and enemies, and also keeping it in the same scene.

All of the characters attacks, and their damages and stuff are in their scripts.

Also, when the battle starts, it automatically adds all nearby entities.

By the way, when writting the code, I commented out a bunch of lines to do things like debugging. You could probably delete those,
but you might want to read them first to tell if it would break the game or not.

Finally, I didn't really know where to start when first making this, so looking back, some of the code kind of sucks.
*/
public class BattleSystem : MonoBehaviour
{
    #region Singleton
    public static BattleSystem instance;

    void Awake()
    {
        //This is for the singleton I think.
        if (instance != null)
        {
            //Debug.LogWarning("More than one instance of BattleSystem found!");
            return;
        }
        instance = this;
    }

    #endregion

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject cameraPrefab;

    public GameObject partyMember2Position;
    public GameObject partyMember3Position;

    public int playerRadius = 10;
    public Vector2 playerCenter;

    public BattleState state;

    public List<GameObject> enemiesInBattle = new List<GameObject>();//List of every enemy in battle.
    public List<GameObject> partyMembersInBattle = new List<GameObject>();//List of every party member, including the player in battle.

    public GameObject currentTurn;//The enemy or party member who's turn it is,
    public GameObject selectedEnemy;//The Enemy the player's selecting.
    public bool selectingEnemy = false;
    public int selectedAttackDMG;
    public string selectedAttack;

    public int playerTurnsLeft;
    public int enemyTurnsLeft;

    //UI
    //-------------------------------------------
    public int cameraSize;
    public int defaultCameraSize;
    public int cameraOffset;

    public TextMeshProUGUI dialogueText;

    public GameObject partyMember1;
    public GameObject partyMember2;
    public GameObject partyMember3;

    public FloatingHealthBar partyMember1HealthBar;
    public FloatingHealthBar partyMember2HealthBar;
    public FloatingHealthBar partyMember3HealthBar;

    public TextMeshProUGUI partyMember1Text;
    public TextMeshProUGUI partyMember2Text;
    public TextMeshProUGUI partyMember3Text;

    public GameObject attackButton1;
    public GameObject attackButton2;
    public GameObject attackButton3;
    public GameObject attackButton4;

    public TextMeshProUGUI attack1;
    public TextMeshProUGUI attack2;
    public TextMeshProUGUI attack3;
    public TextMeshProUGUI attack4;

    public GameObject targetPrefab;
    GameObject theTarget;
    public int selectedEnemyInt;

    public GameObject battleMenu;
    public Component[] interactable;
    //-------------------------------------------

    public bool isInBattle;

    void Start()
    {
        interactable = battleMenu.GetComponentsInChildren<Button>();
    }

    public void StartBattle()
    {
        if(!battleMenu.activeSelf)
            battleMenu.SetActive(true);
        state = BattleState.START;
        isInBattle = true;
        StartCoroutine(SetUpBattle());
    }

    //This just does basic setup for the battle.
    IEnumerator SetUpBattle()
    {

        playerCenter = playerPrefab.transform.position;

        var moveBool = playerPrefab.GetComponent<Movement>();

        moveBool.canMove = false;
        moveBool.inBattle = true;


        
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(playerCenter, playerRadius);//Creates circle which checks for all colliders around player.

        var sumOfX = 0f;
        var sumOfY = 0f;
        var sumOfObjects = 0;

        foreach (Collider2D collider2d in colliderArray)
        {
            if (collider2d.gameObject.CompareTag("Enemy"))//Will run for every enemy detected.
            {
                enemiesInBattle.Add(collider2d.gameObject);//Adds all detected enemies to list of enemies.

                if (collider2d.gameObject.transform.Find("HealthBar") != null)//Checks if HealthBar exists on each enemy detected.
                {
                    var healthBar = collider2d.gameObject.transform.Find("HealthBar");
                    healthBar.gameObject.SetActive(true);
                }

                sumOfX += collider2d.transform.position.x;
                sumOfY += collider2d.transform.position.y;

                sumOfObjects += 1;
            }

            if (collider2d.gameObject.CompareTag("PartyMember") || collider2d.gameObject.CompareTag("Player"))//Will run for every party member detected.
            {
                partyMembersInBattle.Add(collider2d.gameObject);//Adds all detected allies to list of party members.

                sumOfX += collider2d.transform.position.x;
                sumOfY += collider2d.transform.position.y;

                sumOfObjects += 1;

            }
        }

        int partyCount = partyMembersInBattle.Count;

        //NOTE!!!!! 1/10/24: This whole sysetem was supposed to add all the UI, and set up the party, but I forgot that other
        //people would be put as allies later down the line. It would be much more efficient, and the problem could be fixed
        //by reworking it to make it use for loops instead of if statements, atleast I think so.
        if (partyCount == 2)
        {
            partyMember2.gameObject.SetActive(true);

            partyMember1Text.GetComponent<TMPro.TextMeshProUGUI>().text = partyMembersInBattle[0].name;
            partyMember2Text.GetComponent<TMPro.TextMeshProUGUI>().text = partyMembersInBattle[1].name;

            partyMembersInBattle[0].GetComponent<Unit>().healthBar = partyMember1HealthBar;
            partyMembersInBattle[1].GetComponent<Unit>().healthBar = partyMember2HealthBar;

            //partyMembersInBattle[1].gameObject.transform.position = partyMember2Position.gameObject.transform.position;
        }

        if (partyCount == 3)
        {
            partyMember2.gameObject.SetActive(true);
            partyMember3.gameObject.SetActive(true);

            partyMember1Text.GetComponent<TMPro.TextMeshProUGUI>().text = partyMembersInBattle[0].name;
            partyMember2Text.GetComponent<TMPro.TextMeshProUGUI>().text = partyMembersInBattle[1].name;
            partyMember3Text.GetComponent<TMPro.TextMeshProUGUI>().text = partyMembersInBattle[2].name;

            partyMembersInBattle[0].GetComponent<Unit>().healthBar = partyMember1HealthBar;
            partyMembersInBattle[1].GetComponent<Unit>().healthBar = partyMember2HealthBar;
            partyMembersInBattle[2].GetComponent<Unit>().healthBar = partyMember3HealthBar;

            //partyMembersInBattle[1].gameObject.transform.position = partyMember2Position.gameObject.transform.position;
            //partyMembersInBattle[2].gameObject.transform.position = partyMember3Position.gameObject.transform.position;
        }

        var camera = cameraPrefab;

        camera.transform.position = new Vector3(sumOfX / sumOfObjects, sumOfY / sumOfObjects, -10);
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - cameraOffset, - 10);

        camera.GetComponent<Camera>().orthographicSize = cameraSize;


        playerTurnsLeft = partyMembersInBattle.Count;
        enemyTurnsLeft = enemiesInBattle.Count;

        if(enemiesInBattle.Count > 1)
        {
            dialogueText.text = "Enemies approach!";
        }
        else
        {
            dialogueText.text = "An enemy approaches!";
        }

        makeNonInteractable();
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        currentTurn = partyMembersInBattle[0];
        dialogueText.text = "Choose your action.";
        makeInteractable();

    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        var unit = currentTurn.GetComponent<Unit>();

        attack1.GetComponent<TextMeshProUGUI>().text = unit.attack1;
        attack2.GetComponent<TextMeshProUGUI>().text = unit.attack2;
        attack3.GetComponent<TextMeshProUGUI>().text = unit.attack3;
        attack4.GetComponent<TextMeshProUGUI>().text = unit.attack4;

        selectedEnemy = enemiesInBattle[0];

        dialogueText.text = "Choose your attack.";

        //StartCoroutine(PlayerAttack());
    }

    public GameObject createTarget()
    {
        GameObject target = Instantiate(targetPrefab, selectedEnemy.transform.position, Quaternion.identity);

        theTarget = target;

        return target;

    }

    public void OnAttack1()
    {

        if (state != BattleState.PLAYERTURN)
            return;

        var unit = currentTurn.GetComponent<Unit>();

        selectedAttackDMG = unit.attack1DMG;
        selectedAttack = unit.attack1;

        selectingEnemy = true;
        selectedEnemy = enemiesInBattle[0];

        createTarget();

    }

    public void OnAttack2()
    {

        if (state != BattleState.PLAYERTURN)
            return;

        var unit = currentTurn.GetComponent<Unit>();

        selectedAttackDMG = unit.attack2DMG;
        selectedAttack = unit.attack2;

        selectingEnemy = true;
        selectedEnemy = enemiesInBattle[0];

        createTarget();
    }

    public void OnAttack3()
    {

        if (state != BattleState.PLAYERTURN)
            return;

        var unit = currentTurn.GetComponent<Unit>();

        selectedAttackDMG = unit.attack3DMG;
        selectedAttack = unit.attack3;

        selectingEnemy = true;
        selectedEnemy = enemiesInBattle[0];

        createTarget();
    }

    public void OnAttack4()
    {

        if (state != BattleState.PLAYERTURN)
            return;

        var unit = currentTurn.GetComponent<Unit>();

        selectedAttackDMG = unit.attack4DMG;
        selectedAttack = unit.attack4;

        selectingEnemy = true;
        selectedEnemy = enemiesInBattle[0];

        createTarget();

    }

    public void OnAttacksNoButton()
    {
        GameObject target = theTarget;
        Destroy(target);
        selectingEnemy = false;
        selectedEnemy = enemiesInBattle[0];

        dialogueText.text = "Choose your attack.";

    }

    void Update()
    {
        if (!isInBattle)
        {
            cameraPrefab.transform.position = new Vector3(playerPrefab.transform.position.x, playerPrefab.transform.position.y, -10);
        }
        GameObject target = theTarget;

        //This is basically just the logic for the crossair thing when  you're selecting an enemy. I can't belive this took me
        //so long to figure out.
        if (selectingEnemy)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                selectedEnemyInt += 1;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                selectedEnemyInt -= 1;
            }
            if (selectedEnemyInt >= enemiesInBattle.Count)
            {
                selectedEnemyInt = 0;
            }
            if (selectedEnemyInt < 0)
            {
                selectedEnemyInt = enemiesInBattle.Count - 1;
            }
            if (target != null)
            {
                target.transform.position = selectedEnemy.transform.position;
            }
            selectedEnemy = enemiesInBattle[selectedEnemyInt];

        }

    }

    public void OnAttackConformation()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        GameObject target = theTarget;
        Destroy(target);

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        //Attack
        int selectedPartyMemberInt = partyMembersInBattle.IndexOf(currentTurn);
        var enemyUnit = selectedEnemy.GetComponent<Unit>();
        bool isDead = enemyUnit.TakeDamage(selectedAttackDMG);
        dialogueText.text = currentTurn.name + " used " + selectedAttack + " on " + selectedEnemy.name + "!";

        selectingEnemy = false;

        makeNonInteractable();

        playerTurnsLeft -= 1;

        yield return new WaitForSeconds(2f);

        if (playerTurnsLeft <= 0)
        {
            state = BattleState.ENEMYTURN;
            playerTurnsLeft = partyMembersInBattle.Count;

            currentTurn = enemiesInBattle[0];
            makeNonInteractable();
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
        else
        {
            selectedPartyMemberInt += 1;
            if (selectedPartyMemberInt >= partyMembersInBattle.Count)
            {
                selectedPartyMemberInt = 0;//Might be bug?
            }
            currentTurn = partyMembersInBattle[selectedPartyMemberInt];
        }

        if (isDead)
        {
            selectedEnemy.SetActive(false);
            enemiesInBattle.Remove(selectedEnemy);
            selectedEnemy = null;
            if (enemiesInBattle.Count <= 0)
            {
                state = BattleState.WON;
                makeNonInteractable();
                StartCoroutine(EndBattle());
            }

        }

        enemyTurnsLeft = enemiesInBattle.Count;

        selectedEnemy = null;

        makeInteractable();
    }     

    
    
    //This is the enemy AI. It probably could also be reworked to be much more efficient.
    IEnumerator EnemyTurn()
    {
        print(enemyTurnsLeft);
        makeNonInteractable();
        int selectedEnemyInt = enemiesInBattle.IndexOf(currentTurn);
        int selectedPartyMemberIndex = Random.Range(0, partyMembersInBattle.Count);
        GameObject selectedPartyMember = partyMembersInBattle[selectedPartyMemberIndex];

        var enemyUnit = currentTurn.GetComponent<Unit>();
        var playerUnit = selectedPartyMember.GetComponent<Unit>();

        string[] enemyAttacks = { enemyUnit.attack1, enemyUnit.attack2, enemyUnit.attack3, enemyUnit.attack4 };
        int[] enemyAttacksDMG = { enemyUnit.attack1DMG, enemyUnit.attack2DMG, enemyUnit.attack3DMG, enemyUnit.attack4DMG };

        int selectedAttackIndex = Random.Range(0, enemyAttacksDMG.Length);
        string selectedAttack = enemyAttacks[selectedAttackIndex];
        int selectedAttackDMG = enemyAttacksDMG[selectedAttackIndex];

        print(enemyUnit.name);
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(selectedAttackDMG);
        dialogueText.text = currentTurn.name + " used " + selectedAttack + " on " + selectedPartyMember.name + "!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            partyMembersInBattle.Remove(selectedPartyMember);
            selectedPartyMember.SetActive(false);
            selectedPartyMember = null;
            if (partyMembersInBattle.Count <= 0)
            {
                state = BattleState.LOST;
                makeNonInteractable();
                StartCoroutine(EndBattle());
            }
        }

        enemyTurnsLeft -= 1;
        if (enemyTurnsLeft <= 0)
        {
            state = BattleState.PLAYERTURN;
            enemyTurnsLeft = enemiesInBattle.Count;

            makeInteractable();
            PlayerTurn();
        }
        else
        {
            selectedEnemyInt += 1;
            if (selectedEnemyInt >= enemiesInBattle.Count)
            {
                selectedEnemyInt = 0;//Might be bug?
            }
            currentTurn = enemiesInBattle[selectedEnemyInt];
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            print("You Won");
            dialogueText.text = "You won!";
        } else if(state == BattleState.LOST)
        {
            print("You Were Defeated");
            dialogueText.text = "You were defeated...";
        }
        yield return new WaitForSeconds(2f);
        //Exist Battle
        isInBattle = false;
        makeInteractable();
        battleMenu.SetActive(false);
        cameraPrefab.GetComponent<Camera>().orthographicSize = defaultCameraSize;
        playerPrefab.GetComponent<Movement>().canMove = true;
        playerPrefab.GetComponent<Movement>().inBattle = false;

        foreach (GameObject partyMember in partyMembersInBattle)
        {
            if (!partyMember.gameObject.activeSelf)
            {
                partyMember.gameObject.SetActive(true);
            }
        }
    }

    void makeInteractable()
    {
        foreach (Button button in interactable)
            button.interactable = true;
    }

    void makeNonInteractable()
    {
        foreach (Button button in interactable)
            button.interactable = false;
    }


}
