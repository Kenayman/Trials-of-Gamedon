using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private int heartNumber;
    [SerializeField] private Image[] hearts;
    [SerializeField] private float Life;
    [SerializeField] private float lostControl;

    private PlayerController controller;
    private Animator animator;
    private PlayerDash dash;
    private CombatScript combatScript;

    public Sprite fullHeart;
    public Sprite Emptyheart;
    public bool hasDied = false;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        dash = GetComponent<PlayerDash>();
        combatScript = GetComponent<CombatScript>();
    }
    private void Update()
    {
        HealthUI();
        
    }

    // Update is called once per frame
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
}
