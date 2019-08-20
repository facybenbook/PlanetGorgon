using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UnityClient Client { get; set; }
    public ushort PlayerID { get; set; }

    Vector3 lastPosition;
    Vector3 lastRotation;

    Animator animator;
    int speedhash;
    float speed;

    void Start()
    {
        animator = GetComponent<Animator>();
        speedhash = Animator.StringToHash("Speed");
    }

    void Update()
    {
        if (Client == null)
        {
            Debug.LogError("No client assigned to player component!");
            return;
        }

        if (PlayerID == Client.ID)
        {
            // Send Vector Data If Player Moved
            if (Vector3.SqrMagnitude(transform.position - lastPosition) > 0.05f || Vector3.SqrMagnitude(transform.eulerAngles - lastRotation) > 3f)
                SendTransform();
        }
    }

    void FixedUpdate()
    {
        if (Client == null)
        {
            Debug.LogError("No client assigned to player component!");
            return;
        }

        if (PlayerID == Client.ID)
        {
            // Send Animation State Data
            SendAnimationState();
        }
    }

    void SendTransform()
    {
        //Serialize
        using (DarkRiftWriter writer = DarkRiftWriter.Create())
        {
            writer.Write(transform.position.x);
            writer.Write(transform.position.y);
            writer.Write(transform.position.z);
            writer.Write(transform.eulerAngles.x);
            writer.Write(transform.eulerAngles.y);
            writer.Write(transform.eulerAngles.z);

            //Send
            using (Message message = Message.Create(Tags.MovePlayerTag, writer))
                Client.SendMessage(message, SendMode.Unreliable);
        }

        //Store last values sent
        lastPosition = transform.position;
        lastRotation = transform.eulerAngles;
    }

    void SendAnimationState()
    {
        //Get Animator Condition Values
        speed = animator.GetFloat(speedhash);

        //Serialize
        using (DarkRiftWriter writer = DarkRiftWriter.Create())
        {
            writer.Write(speed);

            //Send
            using (Message message = Message.Create(Tags.AnimationPlayerTag, writer))
                Client.SendMessage(message, SendMode.Unreliable);
        }
    }
}