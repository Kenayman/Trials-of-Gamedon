using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private float Life;
    private PlayerController controller;
    [SerializeField] private float lostControl;
    private Animator animator;
    private PlayerDash dash;
    private CombatScript combatScript; 
    public bool hasDied = false;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        dash = GetComponent<PlayerDash>();
        combatScript = GetComponent<CombatScript>();
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
