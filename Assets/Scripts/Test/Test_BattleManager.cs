using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ArcticWolves_Studio {

    public class Test_BattleManager : MonoBehaviour {

        private static Test_BattleManager instance;
        public static Test_BattleManager GetInstance() {
            return instance;
        }

        [SerializeField] private Camera cam;
        private Test_CharacterBattle currentPlayer, currentEnemy;
        [SerializeField] private GameObject targetIcon;
        [SerializeField] private Party party;
        [SerializeField] private List<Test_CharacterBattle> player, enemy;
        public int enemyIndex = 0;
        [SerializeField] private float camOffset;
        
        public List<GameObject> playerTeam = new();
        public List<GameObject> enemyTeam = new();

        [SerializeField] private int playerTurns, enemyTurns;

        [SerializeField] private bool battleStart = false;

        [SerializeField] Vector3[] playerPositions, enemyPositions;
        [SerializeField] Vector3[] playerTeamPositions, enemyTeamPositions;

        private Turn turn;
        private enum Turn {
            PlayerTurn,
            Busy
        }

        private void Awake() {
            instance = this;
            player = new();
            enemy = new();
        }

        private void Start() {            
            party = GameObject.FindGameObjectWithTag("Party").GetComponent<Party>();

            PlayerSingleton._instance.GetComponent<Movement>().canMove = false;

            InitPlayerTeam();
            InitEnemyTeam();
            CreateTarget();
            camOffset = playerTeam.Count;
            battleStart = true;
            playerTurns = playerTeam.Count;
            enemyTurns = enemyTeam.Count;

            turn = Turn.PlayerTurn;
            currentPlayer = player[0];
            currentEnemy = enemy[0];
        }

        private void Update() {
            if (turn == Turn.PlayerTurn) {
                turn = Turn.Busy;
                
            }



            if (Input.GetKeyDown(KeyCode.A)) enemyIndex++;
            if (Input.GetKeyDown(KeyCode.D)) enemyIndex--;
            GameObject target = targetIcon;
                
            if (enemyIndex < 0) {
                enemyIndex = enemy.Count -1;
            }
            if (enemyIndex >= enemy.Count) {
                enemyIndex = 0;
            }
            currentEnemy = enemy[enemyIndex];
            target.transform.position = currentEnemy.transform.position;
            
        }

        private void LateUpdate() {
            CameraSettings();
        }

        private void CameraSettings() {
            cam = Camera.main;

            if (battleStart) {
                cam.orthographicSize = 8;
                cam.transform.position = new Vector3(0, camOffset, cam.transform.position.z);
            } else {
                cam.orthographicSize = 5;
            }
        }

        private Vector3[] GetTeamMemberPositions(List<GameObject> members, Vector3[] positions) {
            for (int i = 0; i < members.Count; i++) {
                positions[i] = members[i].transform.position;
            }
            return positions;
        }

        private void InitPlayerTeam() {
            foreach (var character in party.partyMembers) {
                playerTeam.Add(character);
                player.Add(character.GetComponent<Test_CharacterBattle>());
                
            }

            for (int i = 0; i < playerTeam.Count; i++) {
                playerTeam[i].transform.position = playerPositions[i];
                player[i].GetComponent<Test_CharacterBattle>().SetUp();
            }
        }

        private void InitEnemyTeam() {
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(PlayerSingleton.instance.transform.position, 50f);

            foreach (Collider2D collider2d in colliderArray) {
                if (collider2d.gameObject.CompareTag("Enemy")) {
                    enemyTeam.Add(collider2d.gameObject);
                    enemy.Add(collider2d.GetComponent<Test_CharacterBattle>());

                    if (collider2d.gameObject.transform.Find("HealthBar") != null) {
                        var healthBar = collider2d.gameObject.transform.Find("HealthBar");
                        healthBar.gameObject.SetActive(true);
                    }

                    for (int i = 0; i < enemyTeam.Count; i++) {
                        enemyTeam[i].transform.position = enemyPositions[i];
                        enemy[i].GetComponent<Test_CharacterBattle>().SetUp();
                    }
                }
            }
        }

        public int GetCurrentPlayer() {
            int selected = player.IndexOf(currentPlayer);
            return selected;
        }

        public int GetCurrentEnemy() { 
            int selected = enemy.IndexOf(currentEnemy);
            return selected;
        }

        private GameObject CreateTarget() {
            GameObject target = Instantiate(targetIcon, enemy[0].transform.position,
                Quaternion.identity);
            targetIcon = target;
            return target;
        }

        public void OnAttackConformation() {
            if (turn != Turn.PlayerTurn)
                return;

            GameObject target = targetIcon;
            Destroy(target);

            PlayerAttacks();
        }

        public void PlayerAttacks() {
            int index = GetCurrentPlayer();
            currentPlayer = player[GetCurrentPlayer()];

            currentPlayer.AttackProjectile(enemy[GetCurrentEnemy()], () => {

                for (int i = 0; i < playerTeam.Count; i++) {
                    playerTurns--;
                    index++;
                }
                if (party.partyMembers.Count > 1) {
                    currentPlayer = player[index];
                } else
                    currentPlayer = player[0];
                ChooseTeamTurn();
            });       
        }

        private void EnemyAttack() {
            int playerPosition = Random.Range(0, playerTeam.Count);

            for (int i = 0; i < player.Count; i++) {
                enemy[i].TestAttack(player[playerPosition], () => {
                    enemyTurns--;
                    ChooseTeamTurn(); });
            }            
        }

        private void ChooseTeamTurn() {
            if (IsBattleOver()) {
                return;
            }
            if (playerTurns < 0) {
                turn = Turn.Busy;
                EnemyAttack();
            } else {
                turn = Turn.PlayerTurn;
                playerTurns = playerTeam.Count;
                enemyTurns = enemyTeam.Count;
            }
        }

        private bool IsBattleOver() {
            foreach (var character in player) {
                if (character.IsDead()) {
                    //Defeat
                }
            }
            foreach (var character in enemy) {
                if (character.IsDead()) {
                    return true;
                }
            }
            return false;
        }
    }
}