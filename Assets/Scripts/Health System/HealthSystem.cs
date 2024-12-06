using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    private static HealthSystem instance;

    public static HealthSystem GetHealthSystem() { return instance; }

    public event EventHandler OnHealthChanged, OnDead;

    private int health, maxHealth;

    public HealthSystem(int maxHealth) {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public void SetHealth(int health) {
        this.health = health;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public int Health { get { return health; } }

    public int MaxHealth { get { return maxHealth; } }

    private void Awake() {
        instance = this;
    }

    public float GetHealthPercent() {
        return (float)health / maxHealth;
    }

    public void OnDamage(int dmg) {
        health -= dmg;
        if (health < 0) {
            health = 0;
        }
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        if (health <= 0) Die();
    }

    public void Die() {
        OnDead?.Invoke(this, EventArgs.Empty);
    }

    public bool isDead() {
        return health <= 0;
    }

    public void Heal(int heal) {
        health += heal;
        if (health > maxHealth) health = maxHealth;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void MaxHeal() {
        health = maxHealth;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }
}