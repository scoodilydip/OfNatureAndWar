using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcticWolves_Studio {

    public class Test_BattleManager : MonoBehaviour {

        [SerializeField] private Camera cam;
        [SerializeField] private Party party;
        [SerializeField] private float camOffset;

        public List<GameObject> playerTeam = new();
        public List<GameObject> enemyTeam = new();

        [SerializeField] private bool battleStart = false;

        [SerializeField] Vector3[] playerPositions, enemyPositions;

        private void Start() {
            
            party = GameObject.FindGameObjectWithTag("Party").GetComponent<Party>();


            InitializePlayerTeam();
            InitializeEnemies();
            camOffset = playerTeam.Count;
            battleStart = true;
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

        private void InitializePlayerTeam() {
            foreach (var character in party.partyMembers) {
                playerTeam.Add(character);
            }

            for (int i = 0; i < playerTeam.Count; i++) {
                playerTeam[i].transform.position = playerPositions[i];
            }
        }

        private void InitializeEnemies() {
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(PlayerSingleton.instance.transform.position, 50f);

            foreach (Collider2D collider2d in colliderArray) {
                if (collider2d.gameObject.CompareTag("Enemy")) {
                    enemyTeam.Add(collider2d.gameObject);

                    if (collider2d.gameObject.transform.Find("HealthBar") != null) {
                        var healthBar = collider2d.gameObject.transform.Find("HealthBar");
                        healthBar.gameObject.SetActive(true);
                    }

                    for (int i = 0; i < enemyTeam.Count; i++) {
                        enemyTeam[i].transform.position = enemyPositions[i];
                    }
                }
            }
        }















    }
}