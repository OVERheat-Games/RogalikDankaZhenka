using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    private Rigidbody2D rb;
    private Animator animator;

    // Клавиши управления
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveX = GetAxis(moveLeftKey, moveRightKey);
        float moveY = GetAxis(moveDownKey, moveUpKey);

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        rb.velocity = moveDirection * moveSpeed;

        // Обновление анимаций
        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);
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
}
