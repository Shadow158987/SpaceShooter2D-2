using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
    private float speed;
    private float descentAmount;
    private bool movingRight;

    // Initialiser les paramètres de la rangée
    public void Initialize(float speed, float descentAmount, bool movingRight)
    {
        this.speed = speed;
        this.descentAmount = descentAmount;
        this.movingRight = movingRight;
    }

    void Update()
    {
        // Déplacer la rangée horizontalement
        float movementDirection = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * movementDirection * speed * Time.deltaTime);

        // Vérifier si la rangée atteint le bord de l'écran
        foreach (Transform enemy in transform)
        {
            if (enemy != null) // Vérifier si l'ennemi n'est pas détruit
            {
                if (movingRight && enemy.position.x >= Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 0.5f)
                {
                    ChangeDirection();
                    break;
                }
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
        transform.position += Vector3.down * descentAmount; // Descendre la rangée
    }
}
