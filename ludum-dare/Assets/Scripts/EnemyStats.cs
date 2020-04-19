using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    public int maxLife, Attack, Defence, Life;    

    private bool isHit;
    private float isHitCooldown;
    public float stunCooldown = 0.03f;
    private bool dead;
    [SerializeField]
    private ParticleSystem blood;
    [SerializeField]
    private AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > isHitCooldown && isHit)
        {
            isHit = false;
        }
    }

    public void TakeHit(int damage)
    {
        int damageTaken = damage;
        if (!isHit)
        {
            blood.Play();
            isHit = true;
            isHitCooldown = Time.time + stunCooldown;

            Life -= damageTaken;
            am.Play("dano bandido");
        }

        if (Life <= 0 && !dead)
        {
            Die();
        }
    }

    void Die()
    {
        am.Play("morte bandido");
        Destroy(gameObject);
    }
}
