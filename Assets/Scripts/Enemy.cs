using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float Life;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float timeDeath;

    private CombatScript combatScript;
    private Animator anim;
    private float jumpDmg = 2;
    public Rigidbody2D rb;
    private SpecialAttack Sa;
    private bool isDead;
    public float JumpSpeed => jumpSpeed;
    public bool IsDead => isDead;
    public Transform player;
    private  bool lookingR;
    private PlayerHp playerHp;
    [SerializeField]private int monsterPoints;

    void Start()
    {
        isDead = false;
        anim = GetComponent<Animator>();
        combatScript = GetComponent<CombatScript>();
        rb = GetComponent<Rigidbody2D>();
        Sa = GetComponent<SpecialAttack>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHp = player.GetComponent<PlayerHp>();
    }

    private void Update()
    {
        if(playerHp.Life <= 0)
        {
            Death();
        }
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
        player.GetComponent<PlayerHp>().Die(monsterPoints);
        Destroy(gameObject, timeDeath);
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

    public void Look()
    {
        if ((player.position.x > transform.position.x && !lookingR) || (player.position.x < transform.position.x && lookingR))
        {
            lookingR = !lookingR;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }
}

