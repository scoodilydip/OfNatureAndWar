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
        
    }
}
