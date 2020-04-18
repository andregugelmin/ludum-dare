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
    [SerializeField]
    private float TotalActionTime = 5;

    private float angle;
    private float smoothInputMagnitude;
    private float smoothMoveVelocity;
    private float inputMagnitude;
    private float actionTime;

    private Vector3 velocity;
    private Vector3 inputDirection;

    private new Rigidbody rigidbody;

    private GameObject objTarget;

    [SerializeField]
    private GameObject ActionBar;
    [SerializeField]
    private GameObject Bar;
    [SerializeField]
    private GameObject InteractText;
    [SerializeField]
    private GameObject TextObj;
 
    private TMPro.TextMeshPro Text;

    private CharState charState;
    private enum CharState
    {
        idle, moving, destroyingObj
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Text = TextObj.GetComponent<TMPro.TextMeshPro>();
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
            case CharState.destroyingObj:
                actionTime += 1*Time.deltaTime;
                Bar.transform.localScale = new Vector3(actionTime/5, Bar.transform.localScale.y, Bar.transform.localScale.z);
                
                if (actionTime > TotalActionTime)
                {
                    charState = CharState.idle;
                    Destroy(objTarget);
                    ActionBar.SetActive(false);
                    InteractText.SetActive(false);
                }
                Debug.Log(actionTime);
                break;
        }
        Debug.Log(charState);

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
            case CharState.destroyingObj:
                

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

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "ObjTriggerArea")
        {
            InteractText.SetActive(true);
            InteractText.transform.position = other.transform.position;            

            if (Input.GetButtonDown("Action"))
            {
                if (charState != CharState.destroyingObj)
                {
                    charState = CharState.destroyingObj;
                    actionTime = 0;
                    objTarget = other.transform.parent.gameObject;
                    ActionBar.SetActive(true);
                    ActionBar.transform.position = other.transform.position + new Vector3(0, 2, 0);
                    Text.text = "Press E to cancel";
                }

                else
                {
                    charState = CharState.idle;
                    objTarget = null;
                    ActionBar.SetActive(false);
                    Text.text = "Press E to destroy";
                }
                    
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.name == "ObjTriggerArea")
        {
            InteractText.SetActive(false);
        }
    }

}
