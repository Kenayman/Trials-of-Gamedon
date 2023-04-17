using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float Life;
    private Animator anim;
    private float jumpDmg = 2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakesDmg(float dmgTaker)
    {
        Life -= dmgTaker; 

        if(Life <= 0) {

            Death();
        }

        anim.SetTrigger("damage");
    }

    private void Death()
    {
        anim.SetTrigger("death");
        Destroy(gameObject,1f);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(ContactPoint2D point in collision.contacts) { 
            
                if(point.normal.y <= -0.9)
                {
                    collision.gameObject.GetComponent<PlayerController>().Bounce();
                    
                }
            }
        }
    }
}
