using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTagChecker : MonoBehaviour
{
    void Start()
    {
        // Trouve tous les objets tagués avec "Enemy" et les affiche dans la console
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Debug.Log("Aucun objet avec le tag 'Enemy' trouvé dans la scène.");
        }
        else
        {
            Debug.Log("Objets avec le tag 'Enemy' trouvés :");
            foreach (GameObject enemy in enemies)
            {
                Debug.Log(enemy.name + " - Position: " + enemy.transform.position);
            }
        }
    }
}
