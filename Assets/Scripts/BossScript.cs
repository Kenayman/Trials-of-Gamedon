using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb;
    public Transform player;
    private Animator gameControllerAnimator;
    [SerializeField] private float Life;
    [SerializeField] private Image healthBar;
    [SerializeField] private AudioSource deathAudio;
    private float maxLife;


    private float distance;
    public bool isAlive;


    #region Attack
    [SerializeField] private Transform Hitbox;
    [SerializeField] private float atkRange;
    [SerializeField] private float dmgDealt;
    private int monsterPoints = 1000;
    #endregion

    //[SerializeField] private LifeBar lifeBar;
    void Start()
    {
        isAlive = true;
        maxLife = Life;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameManager");
        gameControllerAnimator = gameControllerObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        animator.SetFloat("Distance", distance);
    }

    public void TakesDmg(float dmgTaker)
    {
        if (isAlive)
        {
            Life -= dmgTaker;

            healthBar.fillAmount = Life / maxLife;

        }


        if (Life <= 0 )
        {
            gameControllerAnimator.SetTrigger("next");
            GetComponent<Collider2D>().enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            isAlive = false;
            animator.SetTrigger("death");
            GameObject audioManager = GameObject.Find("DeathSound");
            AudioSource audioSource = audioManager.GetComponent<AudioSource>();
            audioSource.Play();

        }


        animator.SetTrigger("damage");
    }


    private void Death()

    {
        player.GetComponent<PlayerHp>().Die(monsterPoints);
        Destroy(gameObject);
    }

    private void Atack()
    {
        Collider2D[] obj = Physics2D.OverlapCircleAll(Hitbox.position, atkRange);
        Vector2 attackDirection = (player.transform.position - transform.position).normalized;

        foreach (Collider2D collision in obj)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerHp>().TakesDamage(dmgDealt, attackDirection);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Hitbox.position, atkRange);
    }

}

