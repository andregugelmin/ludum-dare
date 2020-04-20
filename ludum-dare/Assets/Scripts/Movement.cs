using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private AudioManager am;

    [SerializeField]
    private float MoveSpeed = 7;
    [SerializeField]
    private float SmoothMoveTime = .1f;
    [SerializeField]
    private float TurnSpeed = 8;
    [SerializeField]
    private float TotalActionTime = 5;
    [SerializeField]
    private float AttackCooldown;

    private float angle;
    private float smoothInputMagnitude;
    private float smoothMoveVelocity;
    private float inputMagnitude;
    private float actionTime;

    private int noOfClicks = 0;
    private float attackTime = 0;

    private Vector3 velocity;
    private Vector3 inputDirection;

    private new Rigidbody rigidbody;

    private GameObject objTarget;

    [SerializeField]
    private GameObject smoke;

    [SerializeField]
    private GameObject ActionBar;
    [SerializeField]
    private GameObject Bar;
    [SerializeField]
    private GameObject InteractText;
    [SerializeField]
    private GameObject TextObj;
    [SerializeField]
    private BoxCollider attackCollider;
 
    private TMPro.TextMeshPro Text;

    private CharState charState;
    private enum CharState
    {
        idle, moving, destroyingObj, attacking
    }

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Text = TextObj.GetComponent<TMPro.TextMeshPro>();
        animator = GetComponent<Animator>();
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
                if (Input.GetMouseButtonDown(0) && Time.time - attackTime > AttackCooldown)
                {
                    
                    attackTime = Time.time;
                    noOfClicks++;
                    Attack();
                    charState = CharState.attacking;
                    attackCollider.enabled = true;
                    animator.SetBool("isAttacking", true);
                }
                break;
            case CharState.moving:
                checkMovement();
                velocity = transform.forward * MoveSpeed * smoothInputMagnitude;
                if (inputMagnitude == 0)
                {
                    charState = CharState.idle;
                }
                if (Input.GetMouseButtonDown(0) && Time.time - attackTime > AttackCooldown)
                {
                    attackTime = Time.time;
                    noOfClicks++;
                    Attack();
                    charState = CharState.attacking;
                    attackCollider.enabled = true;
                    animator.SetBool("isAttacking", true);
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
                    Instantiate(smoke, objTarget.transform.position, Quaternion.identity);
                    animator.SetBool("isRemovingObstacle", false);
                }
                Debug.Log(actionTime);
                break;
            case CharState.attacking:
                if (Input.GetMouseButtonDown(0))
                {
                    attackTime = Time.time;
                    noOfClicks++;
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
            case CharState.destroyingObj:              

                break;
            case CharState.attacking:

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
        animator.SetFloat("Speed", inputMagnitude);
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
                    animator.SetBool("isRemovingObstacle", true);
                    PlaySound("Cortando");
                }

                else
                {
                    charState = CharState.idle;
                    objTarget = null;
                    ActionBar.SetActive(false);
                    Text.text = "Press E to destroy";
                    animator.SetBool("isRemovingObstacle", false);
                    am.Stop("Cortando");
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


    void Attack()
    {
        PlaySound("Ataque");

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (Physics.Raycast(ray, out hit))
        {
            angle = Mathf.Atan2(hit.point.x - transform.position.x, hit.point.z - transform.position.z) * Mathf.Rad2Deg;
            rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        }
    }

    public void returnAttack()
    {
        noOfClicks = 0;
        charState = CharState.idle;
        attackCollider.enabled = false;
        animator.SetBool("isAttacking", false);
    }

    public void PlaySound(string sound)
    {
        am.Play(sound);
    }

}
