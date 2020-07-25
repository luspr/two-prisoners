using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAttributeManager))]
public class Movement : MonoBehaviour
{
    // Serialized fields
    // [SerializeField]
    // private float forwardMoveSpeed = 1f;
    [SerializeField]
    private float backwardMoveSpeedMultiplier = 0.7f;
    // [SerializeField]
    // private float jumpSpeed = 8f;
    [SerializeField]
    private float turnSpeed = 1f;
    [SerializeField]
    private float airControlSensitivity = 1f;
    [SerializeField]
    private float sidestepSpeed = 1f;
    [SerializeField]
    private float sprintMultiplier = 1.7f;
    [SerializeField]
    private float gravity = 20.0f;
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private float maxStamina = 100f;

    // Private variables
    private bool jumping = false;
    private float currentStamima;
    private Vector3 moveVector = Vector3.zero;
    private CharacterController characterController;
    private Animator animator;
    private Coroutine coroutineRef;
    private bool sprintFlag = true;
    private PlayerAttributeManager playerAttributeManager;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        currentStamima = maxStamina;
        playerAttributeManager = GetComponent<PlayerAttributeManager>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalViewInput = Input.GetAxis("Mouse X");
        float verticalMoveInput = Input.GetAxis("Vertical");
        float horizontalMoveInput = Input.GetAxis("Horizontal");


        //sprint handling
        float sprintMultiplierTemp = 1f;
        if ((Input.GetAxis("Sprint")>0) && (currentStamima > 0) && ((verticalMoveInput != 0) || (horizontalMoveInput != 0)))
        {
            if (sprintFlag == false)
            {
                StopCoroutine(coroutineRef);
            }
            sprintFlag = true;
            sprintMultiplierTemp = sprintMultiplier;
            currentStamima = Mathf.Max(currentStamima - 0.3f, 0f);
        }
        else
        {
            if (sprintFlag == true)
            {
                coroutineRef = StartCoroutine(RechargeStamina());
            }
            sprintFlag = false;
        }

        // Vector3 mvmtVector = new Vector3(hor, 0, ver);


        // Handle rotation
        transform.Rotate(Vector3.up, horizontalViewInput * turnSpeed * Time.deltaTime);

        if (characterController.isGrounded)
        {
            float speed = playerAttributeManager.Speed.Value;            //enter character modifier speed
            //float speed = 5;
            // Debug.Log("Speed " + speed);
            if (verticalMoveInput < 0)
            {
                speed = backwardMoveSpeedMultiplier * speed; 

            }
            // Debug.Log("Speed " + speed);
            // Debug.Log("Jump attr " + playerAttributeManager.Jump.Value);

            moveVector = transform.forward * verticalMoveInput + horizontalMoveInput * transform.right;
            moveVector = moveVector.normalized;
            moveVector *= speed * sprintMultiplierTemp;
            // moveVector += transform.right * sidestepSpeed * horizontalMoveInput;

            animator.SetFloat("ForwardSpeed", verticalMoveInput);
            animator.SetFloat("SidestepSpeed", horizontalMoveInput);
            bool sidestepping = (Mathf.Abs(verticalMoveInput) < 0.9) & (!Mathf.Approximately(horizontalMoveInput, 0));
            animator.SetBool("IsSidestepping", sidestepping);

            if (Input.GetButton("Jump"))
            {
                moveVector.y = playerAttributeManager.Jump.Value;
                //moveVector.y = 15;
                animator.SetBool("IsJumping", true);
                // animator.SetFloat("Jump", -moveVector.y);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }
        }
        else
        {
            // Now my feet don't touch the ground. 
            float y = moveVector.y;
            moveVector = (playerAttributeManager.Speed.Value/5) * airControlSensitivity * sprintMultiplierTemp * verticalMoveInput * transform.forward;
            moveVector += (playerAttributeManager.Speed.Value/5) * airControlSensitivity * sprintMultiplierTemp * horizontalMoveInput * transform.right;
            moveVector.y = y;
        }
        // Always apply gravity to ensure we stay grounded
        moveVector.y -= gravity * Time.deltaTime;
        // Debug.Log("move: " + moveVector);
        characterController.Move(moveVector * Time.deltaTime);

    }

    public float GetStamina()
    {
        return currentStamima;
    }

    public float GetMaxStamina()
    {
        return maxStamina;
    }

    public void ReplenishStamina()
    {
        currentStamima = maxStamina;
    }

    public IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1);

        while (currentStamima < maxStamina)
        {
            currentStamima = Mathf.Min(currentStamima + (0.006f*maxStamina), maxStamina);
            yield return null;
        }

        yield break;
    }
}
