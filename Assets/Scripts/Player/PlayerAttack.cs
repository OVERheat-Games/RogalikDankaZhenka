using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject tearPrefab;
    [SerializeField]
    private float shootCooldown = 0.5f;
    [SerializeField]
    private float shootSpeed = 10.0f;
    [SerializeField]
    private float tearLifetime = 2.0f;

    private float lastShootTime;

    // Клавиши стрельбы
    public KeyCode shootLeftKey = KeyCode.LeftArrow;
    public KeyCode shootRightKey = KeyCode.RightArrow;
    public KeyCode shootUpKey = KeyCode.UpArrow;
    public KeyCode shootDownKey = KeyCode.DownArrow;

    // Эффекты предметов
    private float fireRateBoost = 1.0f;

    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        Vector2 shootDirection = GetShootingDirection();

        if (shootDirection != Vector2.zero && Time.time - lastShootTime >= shootCooldown)
        {
            lastShootTime = Time.time;
            GameObject tear = Instantiate(tearPrefab, transform.position, Quaternion.identity);
            Rigidbody2D tearRb = tear.GetComponent<Rigidbody2D>();
            tearRb.velocity = shootDirection.normalized * shootSpeed * fireRateBoost;
            Destroy(tear, tearLifetime);
        }
    }

    Vector2 GetShootingDirection()
    {
        Vector2 shootDirection = GetDirectionForKey(shootLeftKey, Vector2.left);
        shootDirection += GetDirectionForKey(shootRightKey, Vector2.right);
        shootDirection += GetDirectionForKey(shootUpKey, Vector2.up);
        shootDirection += GetDirectionForKey(shootDownKey, Vector2.down);
        return shootDirection;
    }

    Vector2 GetDirectionForKey(KeyCode key, Vector2 direction)
    {
        if (Input.GetKey(key))
        {
            return direction;
        }
        return Vector2.zero;
    }
}
