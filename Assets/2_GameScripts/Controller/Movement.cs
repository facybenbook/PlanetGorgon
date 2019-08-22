using System;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public GameObject Planet;
    public GameObject PlayerPlaceholder;
    public LayerMask groundedMask;
    public float speed = 4;
    public float jumpForce = 220;
    bool grounded;
    bool OnGround = false;
    bool isrun = false;
    bool isjump = false;
    float gravity = 10;
    float distanceToGround;
    int isrunhash;
    int isjumphash;
    Vector3 Groundnormal;
    Rigidbody rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();
        isrunhash = Animator.StringToHash("run");
        isjumphash = Animator.StringToHash("is_in_air");
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            //MOVEMENT
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

            transform.Translate(0, 0, z);

            //Local Rotation
            float value = Input.GetAxis("Horizontal");
            if (value > 0)
            {
                transform.Rotate(0, 150 * Time.deltaTime, 0);
            }
            if (value < 0)
            {
                transform.Rotate(0, -150 * Time.deltaTime, 0);
            }

            // Run Animation
            value = Input.GetAxis("Vertical");
            if (value > 0)
            { anim.SetBool(isrunhash, true); }
            else { anim.SetBool(isrunhash, false); }

            // Jump
            if (Input.GetButtonDown("Jump"))
            {
                if (grounded) { rb.AddForce(transform.up * jumpForce); }
            }

            // Grounded check
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

            //GRAVITY and ROTATION
            Vector3 gravDirection = (transform.position - Planet.transform.position).normalized;

            if (OnGround == false)
            {
                rb.AddForce(gravDirection * -gravity);
            }

            Quaternion toRotation = Quaternion.FromToRotation(transform.up, Groundnormal) * transform.rotation;
            transform.rotation = toRotation;
        }
        catch(Exception ex)
        { Debug.LogWarning(ex.Message); }
    }
}