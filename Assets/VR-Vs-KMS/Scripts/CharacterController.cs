using System;
using UnityEngine;

class CharacterController : MonoBehaviour
{
    private Vector2 mouseInput;
    private Vector3 playerInput;
    private Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }


    private void Update()
    {
        playerInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        Vector3 MoveVector = transform.TransformDirection(playerInput) * speed;
        rb.velocity = new Vector3(MoveVector.x, rb.velocity.y, MoveVector.z);

        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        transform.Rotate(0f, mouseInput.x * sensitivity, 0f);

    }

}
