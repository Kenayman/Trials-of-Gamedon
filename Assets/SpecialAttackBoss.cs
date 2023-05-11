using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackBoss : StateMachineBehaviour
{
    [SerializeField] private GameObject specialAttack;
    [SerializeField] private float offSetY;
    private BossScript boss;
    private Transform player;
    private BossWalk bossDirection;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss= animator.GetComponent<BossScript>();
        player= boss.player;

        Rotate(animator.transform, player.position);

        Vector2 position = new Vector2(player.position.x +3, player.position.y + offSetY);

        Instantiate(specialAttack, position, Quaternion.identity);
        Debug.Log("Ataque");
        
    }
    public void Rotate(Transform transform, Vector3 objective)
    {
        if (transform.position.x > objective.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (transform.position.x < objective.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
