using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDmg : MonoBehaviour
{
    [SerializeField] private float dmgDealt;
    private bool hasAttacked = false;
    private Animator animator;
    private GameObject player;
    private GameObject mouthHitbox;
    private PlayerHp playerHp;
    private Enemy enemy;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        mouthHitbox = transform.Find("MouthHitbox").gameObject;
        playerHp = player.GetComponent<PlayerHp>();
        enemy = player.GetComponent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");

        if (other.gameObject.CompareTag("Player") && !hasAttacked && !playerHp.hasDied)
        {
            
                hasAttacked = true;
                animator.SetTrigger("attack");

                // Obtener la dirección del ataque del enemigo
                Vector2 attackDirection = (player.transform.position - transform.position).normalized;

                // Multiplicar la dirección por un vector que empuje al jugador hacia atrás en X
                attackDirection = Vector2.Scale(attackDirection, new Vector2(-1f, 1f));

            
              
                player.GetComponent<PlayerHp>().TakesDamage(dmgDealt, attackDirection);
                StartCoroutine(WaitForAnimation());
            
                
           

        }
    }






    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        hasAttacked = false;
    }


}