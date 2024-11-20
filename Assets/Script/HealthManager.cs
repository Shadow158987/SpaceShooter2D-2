using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject Vie1;
    public GameObject Vie2;
    public GameObject Vie3;
    public int maxHealth = 3;
    private int currentHealth;

    // Durée d'invincibilité après avoir pris des dégâts
    public float invincibilityDuration = 2.0f;
    private bool isInvincible = false;
    private float invincibilityTimer = 0;

    // Event pour indiquer quand le joueur est "mort"
    public delegate void PlayerDeathEvent();
    public event PlayerDeathEvent OnPlayerDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        LifeUpdate(); // Mettre à jour l'affichage initial de la vie
    }

    private void Update()
    {
        // Si le joueur est invincible, diminuer le timer d'invincibilité
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false; // Fin de l'invincibilité
            }
        }
    }

    // Méthode pour prendre des dégâts
    public void TakeDamage(int damage)
    {
        // Si le joueur est invincible, ignorer les dégâts
        if (isInvincible) return;

        // Appliquer les dégâts et activer l'invincibilité
        currentHealth -= damage;
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;

        if (currentHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
            currentHealth = 0; // S'assurer que la vie ne soit pas négative
        }

        LifeUpdate();
    }

    // Mise à jour de l'affichage des vies
    private void LifeUpdate()
    {
        Vie1.SetActive(currentHealth >= 1);
        Vie2.SetActive(currentHealth >= 2);
        Vie3.SetActive(currentHealth >= 3);
    }

    // Méthode pour obtenir la santé actuelle (optionnel si tu en as besoin ailleurs)
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    // Pour ajouter une vie 
    public void AddHealth(int amount){

    currentHealth += amount;
    if (currentHealth > maxHealth)
    {
        currentHealth = maxHealth; // Empêche de dépasser le maximum
    }
    LifeUpdate(); // Met à jour l'affichage des vies
   }

}
