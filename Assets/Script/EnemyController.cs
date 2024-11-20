using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 1;
    public GameObject explosionPrefab; // Référence au prefab d'explosion
    public GameObject projectilePrefab; // Prefab du projectile ennemi
    public Transform shootPoint;        // Point d'où le projectile sera tiré
    public float fireRate = 2.0f;       // Intervalle de tir
    private float nextFireTime = 0f;    // Temps jusqu'au prochain tir

    private AudioSource audioSource;    // Référence à l'AudioSource pour le son d'explosion
    private VictoryManager victoryManager; // Référence au VictoryManager
    private Transform player;           // Référence au joueur pour viser

    private void Start()
    {
        // Récupérer la source audio
        audioSource = GetComponent<AudioSource>();

        // Récupérer la référence au VictoryManager dans la scène
        victoryManager = FindObjectOfType<VictoryManager>();

        // Trouver le joueur dans la scène
        player = GameObject.FindGameObjectWithTag("Players")?.transform;
    }

    private void Update()
    {
        // Vérifie si l'ennemi est sorti de l'écran
        if (transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - 1f)
        {
            Destroy(gameObject);
        }

        // Tir automatique à intervalle régulier
        if (Time.time >= nextFireTime)
        {
            ShootAtPlayer();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missile"))
        {
            health--;
            Destroy(other.gameObject); // Détruire le missile

            if (health <= 0)
            {
                Explode();
                Destroy(gameObject, 0.5f); // Détruire l'ennemi après l'explosion et le son
                GameManager.instance.Score(); // Actualise le score quand un enemis meurt

                // Vérifie les conditions de victoire après la destruction de l'ennemi
                if (victoryManager != null)
                {
                    victoryManager.Invoke("CheckVictoryCondition", 3); // Retard de vérification
                }
            }
        }
    }

    private void ShootAtPlayer()
    {
        if (player != null && projectilePrefab != null && shootPoint != null)
        {
            // Calcul de la direction vers le joueur
            Vector2 direction = (player.position - shootPoint.position).normalized;

            // Instancier le projectile et le diriger vers le joueur
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            projectile.transform.up = direction; // Orienter le projectile vers le joueur
        }
    }

    private void Explode()
    {
        // Créer l'explosion à la position de l'ennemi
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 1f); // Détruire l'explosion après 1 seconde
        }

        // Jouer le son d'explosion
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
