using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimwalk : StateMachineBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float baseTime;
    private float movementTime;
    private Transform player;
    private EnemyAtack slime;
    private Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        movementTime = baseTime;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        slime = animator.gameObject.GetComponent<EnemyAtack>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.position, movementSpeed * Time.deltaTime);
            slime.Rotate(player.position);
            movementTime -= Time.deltaTime;


        if(movementTime <= 0 ) 
        {
            animator.SetTrigger("Back");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public void Rotate(Transform transform, Vector3 objective)
    {
        if (transform.position.x < objective.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (transform.position.x > objective.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

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
