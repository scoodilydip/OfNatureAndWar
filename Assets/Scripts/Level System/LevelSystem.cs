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
        expToNextLevel = new int[maxLevel];
    }

    public int GetCurrentExp() {
        return currentExp;
    }

    public int GetExpToNextLevel(int level) {
        if (level < expToNextLevel.Length) {
            return expToNextLevel[level];
        } else {
            Debug.LogError($"LevelSystem > Not Valid Level: {level}");
            return level + 1000;
        }  
    }

    public int GetCurrentLvl() {
        return level;
    }

    public float GetExpBar() {
        if (IsMaxLevel()) {
            return 1f;
        } else {
            return (float)currentExp / GetExpToNextLevel(level);
        }
    }

    public bool IsMaxLevel() {
        return IsMaxLevel(level);
    }

    public bool IsMaxLevel(int level) {
        return level == expToNextLevel.Length - 1;
    }

    public void AddExperience(int experience) {

        if (!IsMaxLevel()) {

            currentExp += experience;

            while (!IsMaxLevel() && currentExp >= GetExpToNextLevel(level)) {
                currentExp -= GetExpToNextLevel(level);
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