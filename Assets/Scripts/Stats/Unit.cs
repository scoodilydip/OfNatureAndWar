using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public string unitName;

    public int unitLevel;

    public float maxHealth;
    public float currentHealth;

    public string attack1;
    public string attack2;
    public string attack3;
    public string attack4;

    public int attack1DMG;
    public int attack2DMG;
    public int attack3DMG;
    public int attack4DMG;

    public FloatingHealthBar healthBar;
    public Sprite sprite;

    private void Awake()
    {
        if(GetComponentInChildren<FloatingHealthBar>() != null)
        {
            healthBar = GetComponentInChildren<FloatingHealthBar>();
        }
    }

    public bool TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
