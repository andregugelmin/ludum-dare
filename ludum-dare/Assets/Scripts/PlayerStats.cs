using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxLifePlayer, Attack;
    public float Life, regenLife, regenCooldown = 1, timeRegenered;


    [SerializeField]
    private ParticleSystem blood;
    [SerializeField]
    private AudioManager am;

    private bool isHit;
    private float isHitCooldown;

    // Start is called before the first frame update
    void Start()
    {
        Life = maxLifePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit(int damage)
    {     
        
        blood.Play();
            

        Life -= damage;
        am.Play("DanoPlayer");
        Debug.Log("Atacked");

        if (Life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        
    }
}
