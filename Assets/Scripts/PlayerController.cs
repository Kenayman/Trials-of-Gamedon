using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float dashPower = 24f;
    private bool canDash=true;
    private float dashCd=1f;
    private float lastDashTime = -1f;


    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float moveForce;
    [SerializeField] private LayerMask jumpable;
    [SerializeField] private float bounceSpeed;

    private bool isPrep = false;

    private bool jumped= false;

    

    private enum MovementState { idle, running, jumping, falling, prep, combatIdle}
    private MovementState state = MovementState.idle;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
    }

    private void Controls()
    {
        MovementState state = this.state;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            playerRb.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerRb.gravityScale = 9;
            StartCoroutine(ResetGravity());
        }

        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }



        float horizontalInput = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(horizontalInput * moveForce, playerRb.velocity.y);

        if (horizontalInput > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
            
            
        }

        else if (horizontalInput < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (playerRb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        if (playerRb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            isPrep = true;
            state = MovementState.prep;
        }
        if (isPrep && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            isPrep = false;
            state = MovementState.combatIdle;
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

    private IEnumerator Dash()
    {
        float dashDuration = 0.5f;
        float dashStartTime = Time.time;

        playerRb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        animator.SetTrigger("Dash");

        // Establecer la última vez que se usó el dash
        lastDashTime = Time.time;

        while (Time.time < dashStartTime + dashDuration)
        {
            // Detectar si hay algo debajo del personaje
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, jumpable);
            if (hit.collider != null)
            {
                // Si hay algo debajo, establecer la velocidad y del personaje en 0
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0f);
            }
            else
            {
                // Si no hay nada debajo, establecer la velocidad y del personaje en un valor negativo para que comience a caer
                playerRb.velocity = new Vector2(playerRb.velocity.x, -10f);
            }

            yield return null;
        }
    }

    public void Bounce()
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, bounceSpeed);
    }
}
