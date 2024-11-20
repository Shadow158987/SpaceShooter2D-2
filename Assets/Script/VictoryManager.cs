using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class VictoryManager : MonoBehaviour{
public List<GameObject> ennemyList = new List<GameObject>();

    public Text victoryText;
    public AudioClip victorySound;
    public AudioSource victoryAudioSource;
    public float restartDelay = 3.0f;
    private bool victoryTriggered = false;

    private void Start()
    {
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(false);
        }
    }

    // Appelle cette méthode chaque fois qu'un ennemi est détruit
public void CheckVictoryCondition()
{
    if (victoryTriggered) return;

    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    ennemyList = enemies.ToList();
    int activeEnemyCount = 0;

    foreach (GameObject enemy in enemies)
    {
        if (enemy.activeInHierarchy && enemy != null) // Compte uniquement les ennemis actifs
        {
            activeEnemyCount++;
            Debug.Log("Objet actif avec le tag 'Enemy' : " + enemy.name + " - Position : " + enemy.transform.position);
        }
    }

    Debug.Log("Nombre d'ennemis actifs restants : " + activeEnemyCount);

    if (activeEnemyCount == 0)
    {
        TriggerVictory();
    }
}



    private void TriggerVictory()
    {
        victoryTriggered = true;
        Debug.Log("Victoire déclenchée !"); // Log pour confirmer que la victoire est déclenchée
        
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(true);
        }

        if (victorySound != null && victoryAudioSource != null)
        {
            victoryAudioSource.clip = victorySound;
            victoryAudioSource.Play();
        }

        Invoke("NextLevelOrRestart", restartDelay);
    }

    private void NextLevelOrRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
