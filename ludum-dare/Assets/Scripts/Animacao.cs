using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacao : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private AudioManager am;

    private Animator animator;

    public BoxCollider attackCollider;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = player.transform.position;
    }
    public void returnAttack()
    {
        player.GetComponent<Movement>().noOfClicks = 0;
        player.GetComponent<Movement>().charState = Movement.CharState.idle;
        animator.SetBool("isAttacking", false);
    }

    public void PlaySound(string sound)
    {
        am.Play(sound);
    }

    public void SetAttackColiderOn()
    {
        attackCollider.enabled = true;
    }

    public void SetAttackColiderOff()
    {
        attackCollider.enabled = false;
    }
}
