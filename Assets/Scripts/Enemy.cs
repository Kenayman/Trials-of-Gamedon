using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float Life;
    [SerializeField] private float jumpSpeed;
    private CombatScript combatScript;
    private Animator anim;
    private float jumpDmg = 2;
    public Rigidbody2D rb;
    private SpecialAttack Sa;
    public float JumpSpeed => jumpSpeed;

    void Start()
    {
        anim = GetComponent<Animator>();
        combatScript = GetComponent<CombatScript>();
        rb = GetComponent<Rigidbody2D>();
        Sa = GetComponent<SpecialAttack>();
    }

    public void TakesDmg(float dmgTaker)
    {
        Life -= dmgTaker;

        if (Life <= 0)
        {
            Death();
        }

        anim.SetTrigger("damage");
    }

    private void Death()
    {
        anim.SetTrigger("death");
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y <= -0.9)
                {
                    collision.gameObject.GetComponent<PlayerController>().Bounce();
                }
            }
        }
    }



    public void JumpAtack()
    {
        rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
    }
}

