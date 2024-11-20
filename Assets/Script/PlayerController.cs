using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 200.0f;
    public GameObject missilePrefab;
    public Transform missileSpawnPoint;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    public KeyCode UP;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;
    public AudioSource SoundMissile;

    private HealthManager healthManager;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
    }

    private void Update()
    {
        if (healthManager.GetCurrentHealth() > 0)
        {
            HandleMovement();
            HandleShooting();
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKey(UP))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(Down))
        {
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(Left))
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(Right))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        position.x = Mathf.Clamp01(position.x);
        position.y = Mathf.Clamp01(position.y);
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }   

    private void Shoot()
    {
        Instantiate(missilePrefab, missileSpawnPoint.position, missileSpawnPoint.rotation);
        SoundMissile.Play();
    }
}

