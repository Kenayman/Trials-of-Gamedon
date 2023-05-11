using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{

    [SerializeField] private GameObject Boss;
    private Animator playerAnimator;
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        Boss.SetActive(false);


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(BossTime());
            StartCoroutine(BossSpawn());
        }

        IEnumerator BossTime()
        {
            
            PlayerController controller = player.GetComponent<PlayerController>();
            PlayerDash dash = player.GetComponent<PlayerDash>();
            CombatScript combatScript = player.GetComponent<CombatScript>();
            SpecialAttack specialAttack = player.GetComponent<SpecialAttack>();

            controller.enabled = false;
            dash.enabled = false;
            combatScript.enabled = false;
            specialAttack.enabled = false;
            playerAnimator.SetTrigger("Surprise");

            yield return new WaitForSeconds(3f);
            controller.enabled = true;
            dash.enabled = true;
            combatScript.enabled = true;
            specialAttack.enabled = true;
            Destroy(gameObject);

        }
        IEnumerator BossSpawn()
        {
            yield return new WaitForSeconds(2f);
            Boss.SetActive(true);

        }
    }
}
