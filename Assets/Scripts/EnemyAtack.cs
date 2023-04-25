using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    private GameObject playerObj;
    [SerializeField] private float distance;

    public Vector3 initialPoint;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        animator = GetComponent<Animator>();
        initialPoint = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, playerObj.transform.position);
        animator.SetFloat("Distance", distance);
    }

    public void Rotate(Vector3 objective)
    {
        if(transform.position.x  < objective.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
