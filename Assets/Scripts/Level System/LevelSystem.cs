using System;
using UnityEngine;

public class LevelSystem : MonoBehaviour {

    public event EventHandler OnExpChanged, OnLevelChanged;
    private int[] expToNextLevel;
    private int currentExp, startingExp; 
    private int level, maxLevel;

    public LevelSystem() { 
        level = 1; //does player(s) start at 0 or 1?
        maxLevel = 999;
        currentExp = 0;
        startingExp = 10;
        expToNextLevel[1] = startingExp;
    }

    public void Start() {
        for (int i = 2; i < expToNextLevel.Length; i++) {
            expToNextLevel[i] += Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f); 
        }
    }

    public int GetCurrentLvl() {
        return level;
    }

    public float GetExpBar() {
        return (float)currentExp / expToNextLevel[level];
    }

    public void AddExperience(int experience) {
        currentExp += experience;
        if (level < maxLevel) {
            while (currentExp >= expToNextLevel[level]) {
                currentExp -= expToNextLevel[level];
                level++;
                //add stat increases

                OnLevelChanged?.Invoke(this, EventArgs.Empty);
            }
            OnExpChanged?.Invoke(this, EventArgs.Empty);
        }
        if (level > maxLevel) {
            level = maxLevel;
        }
    }
}