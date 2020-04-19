using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform bixinho;
    [SerializeField]
    private Transform player;

    private Transform target;

    public bool isAttacking;

    [SerializeField]
    private float distBixo;
    [SerializeField]
    private float distPlayer;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float attackCooldown;

    private float attackTime;
    private float distanceFromPlayer;
    private float distanceFromBixinho;
    private float distanceFromTarget;
    private float angle;

    private Rigidbody rigidbody;

    private CharState charState;
    private enum CharState
    {
        idle, chasing, atacking
    }

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckDistances");
        rigidbody = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        attackTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        switch (charState)
        {
            case CharState.idle:
                animator.SetBool("isAttacking", false);
                animator.SetBool("isMoving", false);
                if (target != null)
                {
                    charState = CharState.chasing;
                }
                    
                break;
            case CharState.chasing:
                animator.SetBool("isAttacking", false);
                animator.SetBool("isMoving", true);
                if (target == null)
                {
                    charState = CharState.idle;
                }
                
                break;
            case CharState.atacking:
                animator.SetBool("isAttacking", true);
                animator.SetBool("isMoving", false);
                break;

        }
        if (distanceFromPlayer < distPlayer)
            target = player;
        else if (distanceFromBixinho < distBixo)
            target = bixinho;
        else
            target = null;

        if (target != null && distanceFromTarget <= attackRange && Time.time - attackTime > attackCooldown)
        {
            charState = CharState.atacking;
        }
        else if(target != null && distanceFromTarget <= attackRange && Time.time - attackTime < attackCooldown)
        {
            charState = CharState.idle;
        }
    }
    void FixedUpdate()
    {
        switch (charState)
        {
            case CharState.idle:
                this.rigidbody.velocity = Vector3.zero;
                this.rigidbody.angularVelocity = Vector3.zero;
                break;
            case CharState.chasing:
                if (target != null && distanceFromTarget > attackRange)
                {
                    angle = Mathf.Atan2(target.position.x - transform.position.x, target.position.z - transform.position.z) * Mathf.Rad2Deg;
                    transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                    rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
                }
                
                 
                break;

            case CharState.atacking:

                break;

        }

    }

    private void OnTriggerStay(Collider collider)
    {
        if (isAttacking && collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Movement>().TakeHit();
        }
    }

    private IEnumerator CheckDistances()
    {
        while (true)
        {
            if(player!=null)
                distanceFromPlayer = (player.transform.position - transform.position).sqrMagnitude;
            if(bixinho!=null)
                distanceFromBixinho = (bixinho.transform.position - transform.position).sqrMagnitude;
            if (target != null)
                distanceFromTarget = (target.transform.position - transform.position).sqrMagnitude;
            yield return new WaitForSeconds(0.1f);
           
        }
    }

    public void EndAttack()
    {
        charState = CharState.idle;
        attackTime = Time.time;
    }

    public void TurnOnAttack()
    {
        isAttacking = true;
    }
    public void TurnOffAttack()
    {
        isAttacking = false;
    }
}
