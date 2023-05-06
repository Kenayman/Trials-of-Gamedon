using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] public int heartNumber = 4;
    [SerializeField] private Image[] hearts;
    [SerializeField] public float Life=4;
    [SerializeField] private float lostControl;

    private PlayerController controller;
    private Animator animator;
    private PlayerDash dash;
    private CombatScript combatScript;
    private SpecialAttack specialAttack;
    private SpawnManager spawnManager;
    private float totalLife;
    private int totalHeart;

    public Sprite fullHeart;
    public Sprite Emptyheart;
    public bool hasDied = false;

    // Start is called before the first frame update
    void Awake()
    {
        totalHeart = heartNumber;
        totalLife = Life;
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        dash = GetComponent<PlayerDash>();
        combatScript = GetComponent<CombatScript>();
        specialAttack = GetComponent<SpecialAttack>();
    }
    private void Update()
    {
        HealthUI();
        
    }

    public void TakesDamage(float dmg)
    {
        Life -= dmg;


    }
    private void Death()
    {
        // Desactivar el componente PlayerController
        controller.enabled = false;
        dash.enabled = false;
        combatScript.enabled = false;
        specialAttack.enabled = false;

        GameObject spawnManagerObj = GameObject.Find("SpawnManager");
        if (spawnManagerObj != null)
        {
            spawnManagerObj.SetActive(false);
        }
        // Ignorar la colisión entre las capas 6 y 7 (jugador y enemigo)
        Physics2D.IgnoreLayerCollision(6, 7, true);


        // Reproducir la animación de muerte y destruir el objeto después de un segundo
        animator.SetTrigger("death");

        hasDied = true;
        



    }

    public void TakesDamage(float dmg, Vector2 posicion)
    {
        Life -= dmg;
        animator.SetTrigger("damagePlayer");
        StartCoroutine(LoseControl());
        StartCoroutine(NoCollision());
        controller.DmgBounce(posicion);
        if (Life <= 0)
        {

            Death();
        }



    }

    public void HealthUI ()
    {
        if (Life > heartNumber)
        {
            Life = heartNumber;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Life)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = Emptyheart;
            }
            if (i < heartNumber)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    IEnumerator NoCollision()
    {
        Physics2D.IgnoreLayerCollision(6,7,true);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(6, 7, false);

    }

    IEnumerator LoseControl()
    {
        controller.canMove = false;
        yield return new WaitForSeconds(lostControl);
        controller.canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("heart"))
        {
            Life = totalLife +1;
            heartNumber = totalHeart+1;
        }

    }
}
