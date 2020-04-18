using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BixinhoMovement : MonoBehaviour
{
    private waypoints waypoints;
    [SerializeField]
    private float moveSpeed;
    private Rigidbody rigidbody;
    private int wpIndex;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        waypoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<waypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints.wp[wpIndex].position, moveSpeed * Time.deltaTime);

        Vector3 dir = waypoints.wp[wpIndex].position - transform.position;
        angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));

        if (Vector3.Distance(transform.position, waypoints.wp[wpIndex].position) < 0.1f){
            if(wpIndex < waypoints.wp.Length - 1)
            {
                wpIndex++;
            }
            else
            {
                Destroy(gameObject);
            }

        }

    }
}
