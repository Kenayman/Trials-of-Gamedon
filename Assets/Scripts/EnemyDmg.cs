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
      
                
           

        }
    }



}