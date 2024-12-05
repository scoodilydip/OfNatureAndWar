using ArcticWolves_Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test_BattleUI : MonoBehaviour {

    private Test_BattleManager battleManager;

    public Action<string, string> attackSelected;
    public event EventHandler moveSelected;

    [SerializeField] private List<GameObject> attackList = new();
    [SerializeField] private List<FloatingHealthBar> healthBarList = new();
    [SerializeField] private List<GameObject> partyMembers = new();
    [SerializeField] private TextMeshProUGUI[] memberNames;

    [SerializeField] private TextMeshProUGUI[] attackText;
    private string[] attackNames;

    [SerializeField] private LayerMask layer;

    private int numberOfAttacks = 0;

    private void Start() {
        battleManager = Test_BattleManager.GetInstance();

        attackList.Clear();
        UpdateAllUI();
    }

    private void FixedUpdate() {

       /* if (Input.GetMouseButtonDown(0) && numberOfAttacks < 2) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up, layer);
            Debug.Log(hit.ToString());

            if (hit.collider. == null) {
                return;
            }
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Attack")) {
                attackList.Add(hitObject);
                numberOfAttacks++;
            }
        }*/
        
    }

    private void UpdateAllUI() {
        UpdateUILists(battleManager.playerTeam);
        GetPlayerAttacks();
    }

    private void UpdateUILists(List<GameObject> list) {
        HealthBarUpdate(partyMembers);
        NameUIUpdate(list);
        PlayerUI(list);
    }

    private void PlayerUI(List<GameObject> listObject) {
        int count = listObject.Count;

        for (int i = 0; i < partyMembers.Count; i++) {
            if (count == partyMembers.Count) {
                partyMembers[i].SetActive(true);
            } else if (i < count) {
                partyMembers[i].SetActive(true);
            }
        }
    }

    private void NameUIUpdate(List<GameObject> member) {

        for (int i = 0; i < member.Count; i++) {
            memberNames[i].text = member[i].name;
        }
    }

    private void HealthBarUpdate(List<GameObject> member) {
        foreach (var character in member) {
            healthBarList.Add(character.GetComponent<FloatingHealthBar>());
        }
    }

    private void StaminaBarUpdate(List<GameObject> member) { } //implement later

    public void SetAttack(Action attackOne, Action attackTwo) {

    }

    public void GetPlayerAttacks() {

        int selected = battleManager.GetCurrentPlayer();

        var stats = battleManager.playerTeam[selected].GetComponent<Unit>();

        attackText[0].text = stats.attack1;
        attackText[1].text = stats.attack2;
        attackText[2].text = stats.attack3;
        attackText[3].text = stats.attack4;
    }
}