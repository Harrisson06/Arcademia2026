using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private Vitals vitals;

    [Header("Basic Movement")]
    [SerializeField] private float maxSpeed = 5f;
    private const float acceleration = 50f;
    private const float deceleration = 70f;
    private Vector2 inputDirection;
    private Vector2 currentVelocity;
    private Vector2 lastMoveDirection;
    private PlayerState playerState;

    [Header("Idle")]
    private float idleTimer = 0f;
    private float idleDelay = 0.3f;

    [Header("Dash")]
    private const float dashForce = 10.0f;
    private bool dashRequested = false;
    private float dashDuration = 0.2f;
    private float dashTimer = 0f;

    [Header("Spin")]
    private bool spinning = false;

    enum PlayerState
    {
        Idle,
        Moving,
        Dash
    }

    private void Awake()
    {
        vitals = GetComponent<Vitals>();    
    }

    void Update()
    {
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
        inputDirection = inputDirection.normalized;

        if (Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.Dash)
            dashRequested = true;

        if (Input.GetKeyDown(KeyCode.LeftShift))
            spinning = true;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            spinning = false;
    }

    void FixedUpdate()
    {
        ApplyMovement();
        HandleDash();
        Spin();
    }

    void ApplyMovement()
    {
        if (playerState == PlayerState.Dash) return;

        if (rb.linearVelocity == Vector2.zero)
        {
            idleTimer += Time.deltaTime;
            
            if (idleTimer >= idleDelay)
            {
                playerState = PlayerState.Idle;
                sr.color = Color.white;
                vitals.GiveStamina(20f * Time.deltaTime);
            }
        }
        else if (playerState != PlayerState.Dash) {
            idleTimer = 0f;
            playerState = PlayerState.Moving;
            sr.color = Color.red;
            vitals.GiveStamina(5f * Time.deltaTime);
        }

        if (inputDirection != Vector2.zero)
        {
            Vector2 targetVelocity = inputDirection * maxSpeed;
            lastMoveDirection = rb.linearVelocity.normalized;
            currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentVelocity = Vector2.MoveTowards(currentVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }

        rb.linearVelocity = currentVelocity;
    }

    void HandleDash()
    {
        if (rb.linearVelocity != Vector2.zero && playerState != PlayerState.Dash)
            lastMoveDirection = rb.linearVelocity.normalized;

        if (dashRequested && vitals.TakeStamina(10f))
        {
            dashRequested = false;
            playerState = PlayerState.Dash;
            dashTimer = dashDuration;
            rb.linearVelocity = lastMoveDirection * dashForce;
            sr.color = Color.blue;
            vitals.TakeStamina(15f);
        }

        if (playerState == PlayerState.Dash)
        {
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0f)
            {
                dashTimer = 0f;
                currentVelocity = rb.linearVelocity;
                playerState = PlayerState.Moving;
            }
        }
    }

    private float currentSpinSpeed = 0f;
    const float spinDeceleration = 180f;
    void Spin()
    {
        if (spinning)
        {
            currentSpinSpeed = 720f;
            transform.Rotate(0f, 0f, currentSpinSpeed * Time.fixedDeltaTime);
        }
        else
        {
            if (currentSpinSpeed > 0f)
            {
                currentSpinSpeed = Mathf.MoveTowards(currentSpinSpeed, 0f, spinDeceleration * Time.fixedDeltaTime);
                transform.Rotate(0f, 0f, currentSpinSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
