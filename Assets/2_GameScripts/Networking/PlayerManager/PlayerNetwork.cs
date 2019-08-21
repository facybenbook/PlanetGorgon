using UnityEngine;

/// <summary>
///     Manages the movement of another player's character.
/// </summary>
internal class PlayerNetwork : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The speed to lerp the player's position")]
    public float moveLerpSpeed;

    [SerializeField]
    [Tooltip("The speed to lerp the player's rotation")]

    Animator animator;

    public float rotateLerpSpeed;
    public Vector3 NewPosition { get; set; }
    public Vector3 NewRotation { get; set; }
    public int speed { get; set; }
    int anim_speedhash = Animator.StringToHash("Speed");

    void Awake()
    {
        //Set initial values
        NewPosition = transform.position;
        NewRotation = transform.eulerAngles;

        // Load Components
        animator = GetComponent<Animator>();       
    }

    void Update()
    {
        //Move and rotate to new values
        transform.position = Vector3.Lerp(transform.position, NewPosition, Time.deltaTime * moveLerpSpeed);
        transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(transform.eulerAngles.x, NewRotation.x, Time.deltaTime * rotateLerpSpeed),
            Mathf.LerpAngle(transform.eulerAngles.y, NewRotation.y, Time.deltaTime * rotateLerpSpeed),
            Mathf.LerpAngle(transform.eulerAngles.z, NewRotation.z, Time.deltaTime * rotateLerpSpeed)
        );

        // Set Animator Floats
        animator.SetFloat(anim_speedhash, speed);
    }
}

