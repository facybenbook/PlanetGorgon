using System.Collections;
using UnityEngine;

public class MoveBehavior_NetworkPlayer : MonoBehaviour
{

    Animator animator;
    Vector3 smoothDeltaPosition = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Vector3 deltaPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        Debug.Log(smooth);
        smoothDeltaPosition = Vector3.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition;

        animator.SetFloat("Speed", velocity.magnitude);

    }
}
