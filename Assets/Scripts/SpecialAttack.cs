using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float chargeTime;
    [SerializeField] private float maxChargeTime;
    [SerializeField] private KeyCode specialAttackKey;



    private float currentChargeTime = 0f;
    private bool isCharging = false;
    private bool canShoot = false;
    private int facingDirection;
    private Enemy enemy;
    private Animator animator;
    private PlayerController playerController;

    public bool IsCharging => isCharging;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(specialAttackKey) && !isCharging)
        {
            isCharging = true;
            animator.SetBool("IsCharging", true);
            animator.SetTrigger("Special");
            

        }

        if (Input.GetKeyUp(specialAttackKey) && isCharging)
        {
            if (isCharging && canShoot)
            {
                animator.SetBool("IsCharging", false);
                animator.SetTrigger("Special");
                LaunchProjectile();
                currentChargeTime = 0f;
                canShoot = false;
            }
            isCharging = false;
        }



        if (isCharging)
        {
            currentChargeTime += Time.deltaTime;
            currentChargeTime = Mathf.Min(currentChargeTime, maxChargeTime);
            canShoot = currentChargeTime >= chargeTime;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput > 0)
        {
            facingDirection = 1;
        }
        else if (horizontalInput < 0)
        {
            facingDirection = -1;
        }
    }
    
    private void LaunchProjectile()
    {
        if (facingDirection > 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = new Vector2(projectileSpeed * 2, 0f);
            Destroy(projectile, projectileLifetime);
        }
        else if (facingDirection < 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, spawnPoint2.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = new Vector2(-projectileSpeed *2 , 0f);
            Destroy(projectile, projectileLifetime);
        }
    }





}
