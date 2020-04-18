using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 7;
    [SerializeField]
    private float SmoothMoveTime = .1f;
    [SerializeField]
    private float TurnSpeed = 8;

    private float angle;
    private float smoothInputMagnitude;
    private float smoothMoveVelocity;
    private float inputMagnitude;

    private Vector3 velocity;
    private Vector3 inputDirection;

    private new Rigidbody rigidbody;

    private CharState charState;
    private enum CharState
    {
        idle, moving
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (charState)
        {

            case CharState.idle:
                checkMovement();
                if (inputMagnitude != 0)
                {
                    charState = CharState.moving;
                }
                break;
            case CharState.moving:
                checkMovement();
                velocity = transform.forward * MoveSpeed * smoothInputMagnitude;
                if (inputMagnitude == 0)
                {
                    charState = CharState.idle;
                }

                break;
        }

    }

    void FixedUpdate()
    {
        switch (charState)
        {
            case CharState.idle:
                rigidbody.MoveRotation(transform.rotation);
                break;
            case CharState.moving:
                rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
                rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
                break;
        }

    }

    void checkMovement()
    {
        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, SmoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * TurnSpeed * inputMagnitude);
    }
}
