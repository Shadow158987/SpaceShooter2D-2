using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public int damage = 3; // Dégâts infligés, définissez-le à au moins la valeur de maxHealth du joueur
    public float speed = 0.5f;
    
    private Vector2 targetDirection;

    private void Start()
    {
        // Trouver le joueur dans la scène
        GameObject player = GameObject.FindGameObjectWithTag("Players");

        // Vérifie si le joueur a été trouvé pour éviter des erreurs
        if (player != null)
        {
            // Calculer la direction initiale vers le joueur
            targetDirection = (player.transform.position - transform.position).normalized;
        }
        else
        {
            Debug.LogError("Le joueur n'a pas été trouvé dans la scène.");
            Destroy(gameObject); // Détruit le missile s'il n'y a pas de joueur
        }
    }

    private void Update()
    {
        // Déplacer le missile dans la direction définie au début
        transform.Translate(targetDirection * speed * Time.deltaTime, Space.World);

        // Détruire le missile s'il sort de l'écran
        if (transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Players"))
        {
            // Accéder au HealthManager du joueur
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Détruire le missile après collision
            Destroy(gameObject);
        }
    }
}
