using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Character", menuName = "New Character", order = 1)]
public class StatsSO : ScriptableObject {

    public Sprite sprite;

    public string characterName;

    public int characterLevel;
    public float exp, expToLevel;

    public int health, maxHealth;

    public int baseDamage, physicalDamage, magicDamage;

    public int armor, physicalArmor, magicArmor;
}