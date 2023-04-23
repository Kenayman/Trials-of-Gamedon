using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController player;
    private float baseGravity;
    private Animator animator;


    [SerializeField] private float dashTime =0.2f;
    [SerializeField] private float dashSpeed = 20;
    [SerializeField] private float timeCanDash;
    [SerializeField] private TrailRenderer trailRenderer;


    private bool isDashing;
    private bool canDash = true;
    public bool IsDashing=> isDashing;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        baseGravity = rb.gravityScale;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash) 
        {
            StartCoroutine(Dash());
            animator.SetTrigger("Dash");
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(dashSpeed * player.Direction, rb.velocity.y);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        rb.gravityScale = baseGravity;
        trailRenderer.emitting = false;

        yield return new WaitForSeconds(timeCanDash);
        canDash = true;
    }

}
