using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private Rigidbody rigidBodyComponent;
    private Animator AnimatorComponent;

    private bool isGrounded;
    private bool jumpKeyWasPressed;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
        AnimatorComponent = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        transform.forward = new Vector3(-horizontalInput, 0, -(Mathf.Abs(horizontalInput) - 1));

        AnimatorComponent.SetFloat("Speed", horizontalInput);
        //AnimatorComponent.SetBool("IsGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector3(horizontalInput * 3, rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z);

        isGrounded = Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length != 0;
        if (jumpKeyWasPressed && isGrounded)
        {
            rigidBodyComponent.AddForce(Vector3.up * 6, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
    }

    // Will use this for collectible items later on
    private void OnTriggerEnter(Collider other)
    {
        // Define game layer in unity. Don't forget to set isTrigger to true
        if(other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
        }
    }
}
