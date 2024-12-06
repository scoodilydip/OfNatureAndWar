using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarUI : MonoBehaviour {

    private LevelSystem levelSystem;

    private TextMeshProUGUI levelText;
    private Image experienceBarImage;

    private void Awake() {
        // get refs
    }

    private void Start() {
        SetLevelSystem(levelSystem);
    }

    private void SetExpBar(float exp) {
        experienceBarImage.fillAmount = exp;
    }

    private void SetLevel(int level) {
        levelText.text = $"Level: {level + 1}";
    }
    private void SetLevelSystem(LevelSystem levelSystem) { 
        this.levelSystem = levelSystem;

        SetLevel(levelSystem.GetCurrentLvl());
        SetExpBar(levelSystem.GetExpBar());

        levelSystem.OnLevelChanged += LevelSystem_OnLevelUp;
        levelSystem.OnExpChanged += LevelSystem_OnExpChanged;
    }

    private void LevelSystem_OnExpChanged(object sender, EventArgs e) {
        SetExpBar(levelSystem.GetExpBar());
    }

    private void LevelSystem_OnLevelUp(object sender, EventArgs e) {
        SetLevel(levelSystem.GetCurrentLvl());
    }
}