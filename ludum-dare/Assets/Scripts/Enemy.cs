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

    private float distanceFromPlayer;
    private float distanceFromBixinho;
    private float distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckDistances");
    }

    // Update is called once per frame
    void Update()
    {
        if (distanceFromPlayer < distPlayer)
            target = player;
        else if (distanceFromBixinho < distBixo)
            target = bixinho;
        else
            target = null;
                
    }
    void FixedUpdate()
    {
        if(target !=null && distanceFromTarget > attackRange)
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

     private IEnumerator CheckDistances()
    {
        while (true)
        {
            distanceFromPlayer = (player.transform.position - transform.position).sqrMagnitude;
            distanceFromBixinho = (bixinho.transform.position - transform.position).sqrMagnitude;
            if (target != null)
                distanceFromTarget = (target.transform.position - transform.position).sqrMagnitude;
            yield return new WaitForSeconds(0.5f);
           
        }
    }
}
