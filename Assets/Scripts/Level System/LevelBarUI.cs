using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarUI : MonoBehaviour {
    /// <summary>
    /// So far the level system only works for a single character
    /// Add list the image, text and is LevelSystem pull from Unit
    /// Add Party ref to determine number of members to find 
    /// Add LevelBarUI to the panel that will open at end of battle.
    /// </summary>
    private LevelSystemAnim levelSystem;
    private LevelSystemAnim levelSystemAnim;

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

    public void SetLevelSystem(LevelSystemAnim levelSystem) { 
        this.levelSystem = levelSystem;
    }

    private void SetLevelSystemAnim(LevelSystemAnim levelSystemAnim) { 
        this.levelSystemAnim = levelSystemAnim;

        SetLevel(levelSystemAnim.GetCurrentLvl());
        SetExpBar(levelSystemAnim.GetCurrentExp());

        levelSystemAnim.OnLevelChanged += LevelSystem_OnLevelUp;
        levelSystemAnim.OnExpChanged += LevelSystem_OnExpChanged;
    }

    private void LevelSystem_OnExpChanged(object sender, EventArgs e) {
        SetExpBar(levelSystemAnim.GetCurrentExp());
    }

    private void LevelSystem_OnLevelUp(object sender, EventArgs e) {
        SetLevel(levelSystemAnim.GetCurrentLvl());
    }
}