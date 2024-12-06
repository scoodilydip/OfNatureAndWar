using System;
using UnityEngine;

public class LevelSystemAnim : MonoBehaviour{

    public event EventHandler OnExpChanged, OnLevelChanged;

    private LevelSystem levelSystem;

    private int level, exp;
    private float updateTimer, updateTimerMax;
    private bool isAnimating;

    public LevelSystemAnim(LevelSystem levelSystemAnim) {
        SetLevelSystem(levelSystem);
        updateTimerMax = .016f;
    }

    private void SetLevelSystem(LevelSystem levelSystem) {
        this.levelSystem = levelSystem;

        level = levelSystem.GetCurrentLvl();
        exp = levelSystem.GetCurrentExp();  

        levelSystem.OnExpChanged += LevelSystem_OnExpChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLvlChanged;
    }

    private void LevelSystem_OnLvlChanged(object sender, EventArgs e) {
        isAnimating = true;
    }

    private void LevelSystem_OnExpChanged(object sender, EventArgs e) {
        isAnimating = true;
    }

    private void Update() {
        if (isAnimating) {
            updateTimer += Time.deltaTime;
            while (updateTimer > updateTimerMax) {
                updateTimer -= updateTimerMax;
                UpdateExpGained();
            }
        }
    }

    private void UpdateExpGained() {
        
        if (level < levelSystem.GetCurrentLvl()) {
            AddExp();
        } else {
            if (exp < levelSystem.GetCurrentExp()) {
                AddExp();
            } else {
                isAnimating = false;
            }
        }
    }

    public float GetExpBar() {
        if (levelSystem.IsMaxLevel()) {
            return 1f;
        } else {
            return (float)exp / levelSystem.GetExpToNextLevel(level);
        }
    }

    public int GetCurrentExp() {
        return exp;
    }

    public int GetExpToNextLevel() {
        return levelSystem.GetExpToNextLevel(level);
    }

    public int GetCurrentLvl() {
        return level;
    }

    private void AddExp() {
        exp++;
        if (exp >= levelSystem.GetExpToNextLevel(level)) {
            level++;
            exp = 0;
            OnLevelChanged?.Invoke(this, EventArgs.Empty);
        }
        OnExpChanged?.Invoke(this, EventArgs.Empty);
    }
}