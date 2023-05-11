using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BossSpecial : MonoBehaviour
{
    private float specialDmg = 1;
    [SerializeField] private Vector2 dimension;
    [SerializeField] private Transform position;
     private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Atack()
    {
        Collider2D[] obj = Physics2D.OverlapBoxAll(position.position, dimension, 0f);
        Vector2 attackDirection = (player.transform.position - transform.position).normalized;

        foreach (Collider2D collision in obj)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerHp>().TakesDamage(specialDmg, attackDirection);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(position.position, dimension);
    }

    private void Death()

    {
        Destroy(gameObject);
    }
}
