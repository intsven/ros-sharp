using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using std_msgs = RosSharp.RosBridgeClient.MessageTypes.Std;
using sensor_msgs = RosSharp.RosBridgeClient.MessageTypes.Sensor;
using UnityEngine.UI;
using System.Linq;

public class TestROS : MonoBehaviour
{

    public string uri = "ws://10.10.101.185:11311";
    private RosSocket rosSocket;
    string subscriptionId = "";

    string publication_id;

    public Text text;
    int count = 0;

    void Start()
    {
        rosSocket = new RosSocket(new
        RosSharp.RosBridgeClient.Protocols.WebSocketNetProtocol(uri)); // 10.189.42.225:9090
        rosSocket.Advertise<std_msgs.String>("/chatter");
        publication_id = rosSocket.Advertise<sensor_msgs.CompressedImage>("/test_image");

        InvokeRepeating("Publish", 0, 1);
        //Subscribe("/chatter");
        
    }

    void Publish()
    {
        string message = "Hi " + count++;
        rosSocket.Publish("/chatter", new std_msgs.String(message));

        // Publication:
        int ImageDataSize = 100;
        sensor_msgs.CompressedImage image = new sensor_msgs.CompressedImage();
        image.header.frame_id = "Test";
        image.header.seq += 1;
        image.data = Enumerable.Repeat((byte)0x20, ImageDataSize).ToArray();
        rosSocket.Publish(publication_id, image);
        
        Debug.Log("Publish: " + message);
    }

    public void Subscribe(string id)
    {
        subscriptionId = rosSocket.Subscribe<std_msgs.String>(id, SubscriptionHandler);
        StartCoroutine(WaitForKey());
    }

    private IEnumerator WaitForKey()
    {
        Debug.Log("Press any key to close...");

        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        Debug.Log("Closed");
        // rosSocket.Close();
    }

    private void SubscriptionHandler(std_msgs.String message)
    {
        Debug.Log("Message received!");
        Debug.Log(message.data);
        
    }
}
