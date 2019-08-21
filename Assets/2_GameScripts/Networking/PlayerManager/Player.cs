using DarkRift;
using DarkRift.Client.Unity;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UnityClient Client { get; set; }
    public ushort PlayerID { get; set; }

    Vector3 lastPosition;
    Vector3 lastRotation;

    Animator animator;
    int speedhash;
    int jumphash;
    int groundedhash;
    int speed;
    byte jump;
    byte grounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        speedhash = Animator.StringToHash("Speed");
        jumphash = Animator.StringToHash("Jump");
        groundedhash = Animator.StringToHash("Grounded");
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
            // Send Vector Data If Player Moved
            if (Vector3.SqrMagnitude(transform.position - lastPosition) > 0.05f || Vector3.SqrMagnitude(transform.eulerAngles - lastRotation) > 3f)
                SendTransform();

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
        speed = (int)Math.Round(animator.GetFloat(speedhash));
        jump = Convert.ToByte(animator.GetBool(jumphash));
        grounded = Convert.ToByte(animator.GetBool(groundedhash));

        //Serialize
        using (DarkRiftWriter writer = DarkRiftWriter.Create())
        {
            writer.Write(speed);
            writer.Write(jump);
            writer.Write(grounded);

            //Send
            using (Message message = Message.Create(Tags.AnimationPlayerTag, writer))
                Client.SendMessage(message, SendMode.Unreliable);
        }
    }
}