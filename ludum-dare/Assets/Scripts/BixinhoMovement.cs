using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BixinhoMovement : MonoBehaviour
{
    private waypoints waypoints;

    [SerializeField]
    private float moveSpeed;
    private float moveSpeedValue;
    [SerializeField]
    private float TurnSpeed = 8;

    private Rigidbody rigidbody;

    private int wpIndex;

    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        waypoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<waypoints>();
        moveSpeedValue = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = waypoints.wp[wpIndex].position - transform.position;
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * TurnSpeed);

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

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints.wp[wpIndex].position, moveSpeedValue * Time.deltaTime);

        rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));

        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward)*2, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2.0f))
            moveSpeedValue = 0;
        else
            moveSpeedValue = moveSpeed;
    }
}
