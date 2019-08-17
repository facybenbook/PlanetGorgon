using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_script : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 2.0f;
    private float gravity = 14f;
    private float verticalVelocity = 10f;
    private CharacterController controller;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        // Move Mouse
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Get Mouse Position
        transform.Rotate(Vector3.up, mouseInput.x * rotationSpeed);


        gameObject.GetComponent<CharacterController>().Move(transform.TransformDirection(input * speed * Time.deltaTime));

    }
}
