using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SpecialAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform Hitbox;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float chargeTime;
    [SerializeField] private float maxChargeTime;
    [SerializeField] private KeyCode specialAttackKey;
    [SerializeField] private Text cooldownText1;
    [SerializeField] private Text cooldownText2;
    [SerializeField] private Text cooldownText3;

    private float currentChargeTime = 0f;
    private bool isCharging = false;
    private bool canShoot = false;
    private int facingDirection;
    private Enemy enemy;
    private Animator animator;
    private PlayerController playerController;
    private float lastSpecialAttackTime = -10f; // comenzar en -10 para permitir el primer uso inmediatamente
    private float cooldownRemaining;

    public bool IsCharging => isCharging;
    public float cooldownTime = 10f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        cooldownRemaining = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(specialAttackKey) && !isCharging && Time.time - lastSpecialAttackTime >= 10f) // agregar la verificación del tiempo aquí
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

                lastSpecialAttackTime = Time.time; // establecer el tiempo del último uso del ataque especial
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

        if (cooldownRemaining > 0f)
        {
            cooldownRemaining -= Time.deltaTime;
            cooldownText1.text = "" + Mathf.CeilToInt(cooldownRemaining).ToString();
            cooldownText2.text = "" + Mathf.CeilToInt(cooldownRemaining).ToString();
            cooldownText3.text = "" + Mathf.CeilToInt(cooldownRemaining).ToString();
        }
        else
        {
            cooldownText1.text = "GO!";
            cooldownText2.text = "GO!";
            cooldownText3.text = "GO!";
        }
    }

    private void LaunchProjectile()
    {
        if (cooldownRemaining <= 0f)
        {
            if (facingDirection > 0)
            {
                GameObject projectile = Instantiate(projectilePrefab, Hitbox.position, Quaternion.identity);
                Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.velocity = new Vector2(projectileSpeed * 2, 0f);
                Destroy(projectile, projectileLifetime);
            }
            else if (facingDirection < 0)
            {
                GameObject projectile = Instantiate(projectilePrefab, Hitbox.position, Quaternion.identity);
                Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
                projectileRb.velocity = new Vector2(-projectileSpeed * 2, 0f);
                Destroy(projectile, projectileLifetime);
            }

            cooldownRemaining = cooldownTime;
        }
    }

}

