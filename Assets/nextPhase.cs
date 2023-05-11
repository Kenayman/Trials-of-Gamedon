using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextPhase : MonoBehaviour
{
    private BossScript boss;
    private Animator animator;

    void Start()
    {

        boss = GetComponent<BossScript>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (boss == null)
        {
            animator.SetTrigger("next");
        }
    }
}

