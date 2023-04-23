using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float punchRatio;
    [SerializeField] private float Damage;
    private Dictionary<Enemy, bool> hitEnemies = new Dictionary<Enemy, bool>();

    void Awake()
    {

    }

    void Update()
    {
        CollisionImpact();
    }

    private void CollisionImpact()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, punchRatio);
        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag("Enemy"))
            {
                Enemy enemy = obj.transform.GetComponent<Enemy>();
                if (enemy != null && !hitEnemies.ContainsKey(enemy))
                {
                    hitEnemies[enemy] = true;
                    enemy.TakesDmg(Damage);
                }
            }
        }
    }
}
