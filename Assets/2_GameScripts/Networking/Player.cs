using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    ///     The DarkRift client to send data though.
    /// </summary>
    public UnityClient Client { get; set; }

    /// <summary>
    ///     The ID of this player.
    /// </summary>
    public ushort PlayerID { get; set; }


    /// <summary>
    ///     The last position our character was at.
    /// </summary>
    Vector3 lastPosition;

    /// <summary>
    ///     The last rotation our character was at.
    /// </summary>
    Vector3 lastRotation;

    void Update()
    {
        if (Client == null)
        {
            Debug.LogError("No client assigned to player component!");
            return;
        }

        if (PlayerID == Client.ID)
        {
            if (Vector3.SqrMagnitude(transform.position - lastPosition) > 0.05f || Vector3.SqrMagnitude(transform.eulerAngles - lastRotation) > 3f)
                SendTransform();
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
}