using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    public float speed = 2.0f; // Vitesse de déplacement horizontale du groupe
    public float descentAmount = 0.5f; // Distance de descente lorsque le groupe atteint un bord
    private bool movingRight = true; // Indique si le groupe se déplace vers la droite

    void Update()
    {
        // Déplacer le groupe horizontalement
        float movementDirection = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * movementDirection * speed * Time.deltaTime);

        // Vérifier si le groupe atteint le bord de l'écran
        foreach (Transform enemy in transform)
        {
            if (enemy != null) // Vérifier si l'ennemi n'est pas détruit
            {
                // Si un ennemi touche le bord droit de l'écran
                if (movingRight && enemy.position.x >= Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 0.5f)
                {
                    ChangeDirection();
                    break;
                }
                // Si un ennemi touche le bord gauche de l'écran
                else if (!movingRight && enemy.position.x <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 0.5f)
                {
                    ChangeDirection();
                    break;
                }
            }
        }
    }

    // Changer de direction et descendre d'un cran
    private void ChangeDirection()
    {
        movingRight = !movingRight; // Inverser la direction
        transform.position += Vector3.down * descentAmount; // Descendre le groupe d'ennemis d'un cran
    }
}