using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    public int maxLifePlayer, Attack, Defence;
    public float Life, regenLife, regenCooldown = 1, timeRegenered;

    private bool isHit;
    private float isHitCooldown;
    public float stunCooldown = 0.03f;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit(int damage)
    {
        int damageTaken = damage;
        if (!isHit)
        {
            isHit = true;
            isHitCooldown = Time.time + stunCooldown;

            Life -= damageTaken;
        }

        if (Life <= 0 && !dead)
        {
            Die();
        }
    }

    void Die()
    {
    }
}
