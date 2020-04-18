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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckDistances");
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (charState)
        {
            case CharState.idle:
                if (target != null)
                    charState = CharState.chasing;
                break;
            case CharState.chasing:
                if(target == null)
                {
                    charState = CharState.idle;
                }
                break;
            case CharState.atacking:
                break;

        }
        if (distanceFromPlayer < distPlayer)
            target = player;
        else if (distanceFromBixinho < distBixo)
            target = bixinho;
        else
            target = null;
                
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

     private IEnumerator CheckDistances()
    {
        while (true)
        {
            distanceFromPlayer = (player.transform.position - transform.position).sqrMagnitude;
            distanceFromBixinho = (bixinho.transform.position - transform.position).sqrMagnitude;
            if (target != null)
                distanceFromTarget = (target.transform.position - transform.position).sqrMagnitude;
            yield return new WaitForSeconds(0.1f);
           
        }
    }
}
