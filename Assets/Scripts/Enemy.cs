using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float Life;
    private Animator anim;
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
    }

    private void Death()
    {
        anim.SetTrigger("death");
        Destroy(gameObject,1f);
        
    }
}
