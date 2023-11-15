using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private GameObject tearPrefab;
    [SerializeField]
    private float shootCooldown = 0.5f;
    [SerializeField]
    private float shootSpeed = 10.0f;
    [SerializeField]
    private float tearLifetime = 2.0f;
    [SerializeField]
    private float verticalLookDirection = 0.0f;
    [SerializeField]
    private float lookDirection = 0.0f;

    private float lastShootTime;

    private Rigidbody2D rb;

    private PauseMenu pauseMenu;

    private Animator headAnimator;
    private Animator bodyAnimator;
    private Transform headTransform;

    private bool isMovingHorizontal;
    private bool isMovingVertical;

    // Клавиши управления
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;

    // Клавиши стрельбы
    public KeyCode shootLeftKey = KeyCode.LeftArrow;
    public KeyCode shootRightKey = KeyCode.RightArrow;
    public KeyCode shootUpKey = KeyCode.UpArrow;
    public KeyCode shootDownKey = KeyCode.DownArrow;

    // Эффекты предметов
    private float damageBoost = 1.0f;
    private float fireRateBoost = 1.0f;

    void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        rb = GetComponent<Rigidbody2D>();

        headAnimator = transform.Find("Head").GetComponent<Animator>();
        headTransform = transform.Find("Head");

        bodyAnimator = transform.Find("Body").GetComponent<Animator>();    
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        float moveX = GetAxis(moveLeftKey, moveRightKey);
        float moveY = GetAxis(moveDownKey, moveUpKey);

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        rb.velocity = moveDirection * moveSpeed;

        // Устанавливаем параметры анимации для головы
        headAnimator.SetBool("IsMovingHorizontal", Mathf.Abs(moveX) > 0);
        headAnimator.SetBool("IsMovingVertical", Mathf.Abs(moveY) > 0);

        // Определяем направление взгляда и устанавливаем параметр анимации
        if (moveX > 0.1f)
        {
            // Вправо
            headAnimator.SetFloat("LookDirection", 1f);
        }
        else if (moveX < -0.1f)
        {
            // Влево
            headAnimator.SetFloat("LookDirection", -1f);
        }
        else
        {
            // Стоим на месте, не двигаемся по горизонтали
            headAnimator.SetFloat("LookDirection", 0f);
        }

        // Определяем вертикальное направление
        if (moveY > 0.1f)
        {
            verticalLookDirection = 1f;
        }
        else if (moveY < -0.1f)
        {
            verticalLookDirection = -1f;
        }
        else
        {
            verticalLookDirection = 0f;
        }

        // Устанавливаем параметр для аниматора
        headAnimator.SetFloat("VerticalLookDirection", verticalLookDirection);

        // Устанавливаем параметры анимации для тела
        bodyAnimator.SetBool("IsMovingHorizontal", Mathf.Abs(moveX) > 0 || Mathf.Abs(moveY) > 0);
        bodyAnimator.SetBool("IsMovingVertical", Mathf.Abs(moveY) > 0);
    }
    
    void Shoot()
    {
        if (pauseMenu != null && pauseMenu.IsPaused)
        {
            return;
        }
        
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
        
    }

    float GetAxis(KeyCode negativeKey, KeyCode positiveKey)
    {
        float axis = 0;

        if (Input.GetKey(positiveKey))
        {
            axis = 1;
        }
        else if (Input.GetKey(negativeKey))
        {
            axis = -1;
        }

        return axis;
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

    // // Применение эффектов предметов
    // public void ApplyItemEffect(Item item)
    // {
    //     damageBoost += item.DamageBoost;
    //     fireRateBoost += item.FireRateBoost;
    //     // Другие эффекты предметов
    // }

 

}