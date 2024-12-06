using System;
using UnityEngine;

public class Level : MonoBehaviour {
    
    private LevelSystem levelSystem;

    //[SerializeField] private GameObject pfLvlUpEffect; Move to GameAssets after creating the Object.

    public void SetLevelSystem(LevelSystem levelSystem) { 
        this.levelSystem = levelSystem;

        levelSystem.OnLevelChanged += LevelSystem_OnLvlChanged;
    }

    private void LevelSystem_OnLvlChanged(object sender, EventArgs e) {
        /*
         *Spawn particles
         *Victory Animation
         *Character Flash
         *verify with scood
         */
        
    }

    private void SpawnLvlUpEffect() {
        //GameObject particles = Instantiate(pfLvlUpEffect, transform.position, Quaternion.identity);
        //add Timer/Destroy here or on the particle system
    }
}