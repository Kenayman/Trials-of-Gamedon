using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    #region Variables
    private GameObject playerObj;
    [SerializeField] private float distance;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool canAttack;
    private PlayerHp playerHp;
    private SpawnManager spawnManager;
    public Vector3 initialPoint;
    private Enemy enemy;
    #endregion

    #region Attack
    [SerializeField] private Transform Hitbox;
    [SerializeField] private float atkRange;
    [SerializeField] private float dmgDealt;
    #endregion


    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerHp= playerObj.GetComponent<PlayerHp>(); 
        initialPoint = transform.position;
        spawnManager = GetComponent<SpawnManager>();
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, playerObj.transform.position);
        animator.SetFloat("Distance", distance);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            // Si la animación de ataque está activa, establecer la variable canAttackHands en verdadero
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }


    public void Rotate(Vector3 objective)
    {
        if (transform.position.x < objective.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (transform.position.x > objective.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    private void BlobbyAttack()
    {
        
        DealExplosionDamage(5f, 1f); // deal explosion damage to nearby enemies
    }

    public void DealExplosionDamage(float explosionRange, float explosionDamage)
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach (Collider2D enemyCollider in nearbyEnemies)
        {
            if (enemyCollider.CompareTag("Enemy"))
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();
                if (enemy != null && !enemy.IsDead)
                {
                    enemy.TakesDmg(explosionDamage);
                }
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canAttack && collision.gameObject == playerObj && playerHp.hasDied == false && spawnManager.isPlayerAlive)
        {
            // Atacar al jugador
            animator.SetTrigger("attack");
            Vector2 attackDirection = (playerObj.transform.position - transform.position).normalized;
            attackDirection = Vector2.Scale(attackDirection, new Vector2(-1f, 1f));
            playerHp.GetComponent<PlayerHp>().TakesDamage(dmgDealt, attackDirection);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == playerObj)
        {
            // El enemigo ya no puede atacar al jugador
            canAttack = false;
        }
    }

    private void Atack()
    {
        Collider2D[] obj = Physics2D.OverlapCircleAll(Hitbox.position, atkRange);
        Vector2 attackDirection = (playerObj.transform.position - transform.position).normalized;

        foreach (Collider2D collision in obj)
        {
            if(collision.CompareTag("Player")) 
            {
                playerObj.GetComponent<PlayerHp>().TakesDamage(dmgDealt, attackDirection);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Hitbox.position, atkRange);
    }
}