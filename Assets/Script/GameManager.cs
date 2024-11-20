using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text gameOverText;
    public AudioClip gameOverSound;
    public AudioSource gameOverAudioSource;
    public float restartDelay = 3.0f;
    public static GameManager instance;

    public int GameScore;
    public Text ScoreText;

    public int pointsForExtraLife = 100; // Points nécessaires pour gagner une vie
    private int pointsSinceLastLife = 0; // Points accumulés depuis la dernière vie donnée

    private bool gameOverTriggered = false;
    private HealthManager healthManager;

    private void Start()
    {
        instance = this;

        gameOverText.gameObject.SetActive(false);

        // Trouver et stocker la référence au HealthManager
        healthManager = FindObjectOfType<HealthManager>();

        // S'abonner à l'événement OnPlayerDeath
        if (healthManager != null)
        {
            healthManager.OnPlayerDeath += TriggerGameOver;
        }
    }

    // Méthode pour déclencher le Game Over
    public void TriggerGameOver()
    {
        if (gameOverTriggered) return;

        gameOverTriggered = true;
        gameOverText.gameObject.SetActive(true);

        // Jouer le son de Game Over
        if (gameOverSound != null && gameOverAudioSource != null)
        {
            gameOverAudioSource.clip = gameOverSound;
            gameOverAudioSource.Play();
        }

        // Arrêter le temps (le jeu est figé)
        Time.timeScale = 0;

        // Lancer le redémarrage après un délai
        StartCoroutine(RestartGameAfterDelay());
    }

    // Coroutine pour redémarrer après un délai
    private IEnumerator RestartGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(restartDelay); // Attend en temps réel, même si Time.timeScale est 0

        Time.timeScale = 1; // Remettre le temps à la normale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recharger la scène actuelle
    }

    private void OnDestroy()
    {
        // Se désabonner de l'événement pour éviter les erreurs si le GameManager est détruit
        if (healthManager != null)
        {
            healthManager.OnPlayerDeath -= TriggerGameOver;
        }
    }

    // Méthode pour ajouter des points
    public void Score()
    {
        GameScore += 10;
        ScoreText.text = GameScore.ToString();

        // Ajouter les points à l'accumulateur pour une vie supplémentaire
        pointsSinceLastLife += 10;

        // Vérifier si le joueur a accumulé suffisamment de points pour obtenir une vie
        if (pointsSinceLastLife >= pointsForExtraLife)
        {
            pointsSinceLastLife -= pointsForExtraLife;

            // Ajouter une vie si le joueur n'a pas déjà le maximum
            if (healthManager != null && healthManager.GetCurrentHealth() < healthManager.maxHealth)
            {
                healthManager.AddHealth(1);
                Debug.Log("Vie bonus ajoutée !");
            }
        }
    }
}
