using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public GameObject tearPrefab;
    public float shootCooldown = 0.5f;
    public float shootSpeed = 10.0f;
    public float tearLifetime = 2.0f;
    private float lastShootTime;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Управление движением
        float moveX = 0;
        float moveY = 0;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        rb.velocity = moveDirection * moveSpeed;

        // Управление выстрелами
        Vector2 shootDirection = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            shootDirection += Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            shootDirection += Vector2.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            shootDirection += Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            shootDirection += Vector2.down;
        }

        if (shootDirection != Vector2.zero && Time.time - lastShootTime >= shootCooldown)
        {
            Shoot(shootDirection.normalized);
        }
    }

    void Shoot(Vector2 shootDirection)
    {
        lastShootTime = Time.time;
        GameObject tear = Instantiate(tearPrefab, transform.position, Quaternion.identity);
        Rigidbody2D tearRb = tear.GetComponent<Rigidbody2D>();
        tearRb.velocity = shootDirection * shootSpeed;
        Destroy(tear, tearLifetime);
    }
}



