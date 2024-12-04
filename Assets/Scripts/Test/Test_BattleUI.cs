using ArcticWolves_Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test_BattleUI : MonoBehaviour {

    private Test_BattleManager battleManager;

    public Action<string, string> attackSelected;

    [SerializeField] private List<FloatingHealthBar> healthBarList = new();
    [SerializeField] private List<GameObject> partyMembers = new();
    [SerializeField] private TextMeshProUGUI[] memberNames;


    private void Start() {
        battleManager = Test_BattleManager.GetInstance();
        
        UpdateAllUI();
    }

    private void UpdateAllUI() {
        UpdateUILists(battleManager.playerTeam);
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

    private void StaminaBarUpdate(List<GameObject> member) {  } //implement later

    public void SelectAttack(string attackName, int attackDamage) {

        int selected = battleManager.GetCurrentPlayer();

        var stats = battleManager.playerTeam[selected].GetComponent<Unit>();
        attackName = stats.attack1;
        attackDamage = stats.attack1DMG;
    }
}