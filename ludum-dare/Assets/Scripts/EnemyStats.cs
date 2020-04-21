using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    public float maxLife, Life;
    public int Attack;
    private bool isHit;
    private float isHitCooldown;
    public float stunCooldown = 0.03f;
    private bool dead;
    [SerializeField]
    private ParticleSystem blood;
    [SerializeField]
    private AudioManager am;
    [SerializeField]
    private GameObject LifeBar;
    [SerializeField]
    private GameObject Bar;

    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
        Life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > isHitCooldown && isHit)
        {
            isHit = false;
        }
        if (!LifeBar.activeInHierarchy && Life<maxLife)
        {
            LifeBar.SetActive(true);
        }
        Bar.transform.localScale = new Vector3(Life / maxLife, Bar.transform.localScale.y, Bar.transform.localScale.z);
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
            am.Play("DanoBandido");
        }

        if (Life <= 0 && !dead)
        {
            Die();
        }

        
    }

    void Die()
    {
        am.Play("MorteBandido");
        Destroy(gameObject);
    }
}
