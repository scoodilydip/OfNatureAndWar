using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    /// summary
    /// 
    /// This script will handle each of the allied player character visuals
    /// i.e. armor, weapons, and cosmetics
    /// player spawn
    /// transform position
    /// 
    /// 

    public enum EquipArmor {
        Helmet,
        Chestplate,
        Vambraces,
        Waist,
        Greaves
    }

    public enum EquipWeapon {
        Sword,
        ShortSword,
        BroadSword,
        LongSword,
        GreatSword,
        Dagger,
        Spear,
        Polearm,

    }

    protected SpriteRenderer sprite;
    protected Transform leftHand, rightHand;
    
}