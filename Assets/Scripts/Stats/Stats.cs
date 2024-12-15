using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public Type type;
    public SubType subType;
    public StatsSO stats;

    public enum Type {
        Player,
        Tank,
        Mage,
        Ranger,
        NPC,
        Merchant,
        NONE
    }

    public enum SubType {
        /// <summary>
        /// Additional types for each of the characters
        /// </summary>
        NONE
    }
    private void Start() {
        //make the stats
        AssignStatsBasedOnType(type);
    }

    private void AssignStatsBasedOnType(Type entityTpye) {
        switch (entityTpye) {
            case Type.Player:
            stats = Resources.Load<StatsSO>("PlayerStats");
            break; 
            case Type.Tank:
            stats = Resources.Load<StatsSO>("TankStats");
            break;
            case Type.Mage:
            stats = Resources.Load<StatsSO>("MageStats");
            break;
            case Type.Ranger:
            stats = Resources.Load<StatsSO>("RangerStats");
            break;
            case Type.NPC:
            stats = Resources.Load<StatsSO>("NPCStats");
            break;
            case Type.Merchant:
            stats = Resources.Load<StatsSO>("MerchantStats");
            break;
            default: 
            stats = null;
            break;
        }
    }

    private void TypeBonus() {
        if (type != Type.NONE) {
            switch (type) {
                case Type.Player:
                //Add bonus for Player
                break;
                case Type.Tank:
                //Add bonus for Tank
                break;
                case Type.Mage: 
                //Add bonus for Mage
                break;
                case Type.Ranger:
                //Add bonus for Ranger
                break;
                default: 
                break;

            }
        }
    }

}