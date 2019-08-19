using UnityEngine;

public class MoveBehavior_NetworkPlayer : MonoBehaviour
{

    Animator animator;
    Rigidbody rigidbody;
    public float speed;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var vel = rigidbody.velocity;      //to get a Vector3 representation of the velocity
        speed = vel.magnitude;             // to get magnitude
        animator.SetFloat("speed", speed);
    }    
}
