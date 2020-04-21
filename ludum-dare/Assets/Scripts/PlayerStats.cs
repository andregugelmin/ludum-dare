using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxLifePlayer, Attack;
    public float Life, regenLife, regenCooldown = 1, timeRegenered;


    [SerializeField]
    private ParticleSystem blood;
    [SerializeField]
    private AudioManager am;
    [SerializeField]
    private GameObject animacao;

    private bool isHit;
    private float isHitCooldown;

    [SerializeField]
    private Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        Life = maxLifePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = (Life / maxLifePlayer);
    }

    public void TakeHit(int damage)
    {
        if (gameObject.GetComponent<Movement>().charState == Movement.CharState.destroyingObj)
        {
            gameObject.GetComponent<Movement>().stopAction();
        }
        blood.Play();
        gameObject.GetComponent<Movement>().charState = Movement.CharState.idle;
        animacao.GetComponent<Animacao>().returnAttack();
        animacao.GetComponent<Animacao>().SetAttackColiderOff();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
