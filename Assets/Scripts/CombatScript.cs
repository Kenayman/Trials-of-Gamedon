using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    [SerializeField] public Transform combatController;
    [SerializeField] public Transform combatController2;
    [SerializeField] private float punchRatio;
    [SerializeField] private float damage;
    [SerializeField] private float speedAtack;
    [SerializeField] private float speedNextAtack;
    private bool isAttacking;

    private int currentAtack = 0;
    private Animator animator;
    private PlayerController playerController;

    private int facingDirection = 1; // 1 para derecha, -1 para izquierda

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = transform.GetComponent<PlayerController>();

    }

    private void Update()
    {
        if(speedNextAtack > 0)
        {
            speedNextAtack  -= Time.deltaTime;
        }
        // Detectar la dirección actual del personaje
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput > 0)
        {
            facingDirection = 1;
            Debug.Log(facingDirection);

        }
        else if (horizontalInput < 0)
        {
            facingDirection = -1;
            Debug.Log(facingDirection);
        }


        if (Input.GetButtonDown("Fire1") && speedNextAtack<=0)
        {
            Punch();
            animator.SetInteger("counter", currentAtack);
            speedNextAtack = speedAtack;
        }
        else if (Input.GetButtonDown("Fire2") && speedNextAtack <= 0)
        {
            
                Heavy();
                speedNextAtack = speedAtack;

            
        }
    }

    private void Punch()
    {
        damage = 2;
        if (currentAtack == 0)
        {
            animator.SetTrigger("Atack");
            animator.SetInteger("counter", currentAtack);
            currentAtack++;
        }
        else if (currentAtack == 1)
        {
            animator.SetTrigger("Atack");
            animator.SetInteger("counter", currentAtack);
            currentAtack = 0;
        }

        StartCoroutine(ActivateDamage(0f));





    }

    private void Heavy()
    {
        if (playerController.IsGrounded()) 
        {
            damage = 3;
            animator.SetTrigger("Heavy");
            StartCoroutine(ActivateDamage(0f));


        }
        else if(!playerController.IsGrounded())
        {
            damage = 5;
            animator.SetTrigger("heavyDown");
            StartCoroutine(ActivateDamage(0.5f));
            playerController.playerRb.gravityScale = 9f;
            StartCoroutine(ResetGravity());
        }
        

    }
    public IEnumerator ResetGravity()
    {
        yield return new WaitForSeconds(.5f); // espera por 2 segundos
        playerController.playerRb.gravityScale = 3f; // restaura la gravedad a su valor original
    }

    private void CollisionImpact()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(combatController.position, punchRatio);
        Collider2D[] objects2 = Physics2D.OverlapCircleAll(combatController2.position, punchRatio);

        if (facingDirection == 1)
        {
            foreach (Collider2D obj in objects)
            {
                if (obj.CompareTag("Enemy"))
                {
                    obj.transform.GetComponent<Enemy>().TakesDmg(damage);
                }
            }
        }
        if (facingDirection == -1)
        {
            foreach (Collider2D obj in objects2)
            {
                if (obj.CompareTag("Enemy"))
                {
                    obj.transform.GetComponent<Enemy>().TakesDmg(damage);
                }
            }
        }
    }
    private IEnumerator ActivateDamage(float delay)
    {
        yield return new WaitForSeconds(delay);
        CollisionImpact();
    }


}
