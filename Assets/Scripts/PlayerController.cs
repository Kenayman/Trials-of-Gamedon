using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float direction;
    private PlayerDash playerDash;
    private SpecialAttack sp;
    private float HorizontalInput;
    private bool lookingR = true;
    public bool canMove = true;


    public float Direction => direction;


    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask jumpable;
    [SerializeField] private float bounceSpeed;
    [SerializeField] private Vector2 bounceForce;
    [SerializeField] private float movementSmoothing;
    private Vector3 Speed = Vector3.zero;
    public float JumpSpeed => jumpSpeed;

    private enum MovementState { idle, running, jumping, falling, prep, combatIdle }
    private MovementState state = MovementState.idle;
    // Start is called before the first frame update
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        playerDash = GetComponent<PlayerDash>();
        sp = GetComponent<SpecialAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("Horizontal", Mathf.Abs(HorizontalInput));
        if (!playerDash.IsDashing && canMove && !sp.IsCharging)
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        if (!playerDash.IsDashing && canMove && !sp.IsCharging)
        {
            Move(HorizontalInput * Time.deltaTime);
        }



    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            playerRb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerRb.gravityScale = 9;
            StartCoroutine(ResetGravity());
        }

    }
    private void Move(float horizontalInput)
    {
        playerRb.velocity = new Vector2(horizontalInput * moveSpeed, playerRb.velocity.y);

        if (horizontalInput > 0f && !lookingR)
        {
            state = MovementState.running;
            direction = 1;

            Rotate();
        }
        else if (horizontalInput < 0f  && lookingR)
        {
            state = MovementState.running;
            direction = -1;

            Rotate();
        }
        else
        {
            state = MovementState.idle;
        }

        if (playerRb.velocity.y > 1f)
        {
            state = MovementState.jumping;
        }
        if (playerRb.velocity.y < -3f)
        {
            state = MovementState.falling;
        }
        animator.SetInteger("state", (int)state);
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 1f, jumpable);


    }

    public IEnumerator ResetGravity()
    {
        yield return new WaitForSeconds(.5f); // espera por 2 segundos
        playerRb.gravityScale = 3f; // restaura la gravedad a su valor original
    }

    public void Bounce()
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, bounceSpeed);
    }

    public void DmgBounce(Vector2 dmgPoint)
    {
        playerRb.velocity = new Vector2(-bounceForce.x * dmgPoint.x, bounceForce.y);
    }
    private void Rotate()
    {
        lookingR = !lookingR;
        transform.eulerAngles= new Vector3(0, transform.eulerAngles.y + 180, 0);
    }
}
