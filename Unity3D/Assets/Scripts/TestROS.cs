using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using std_msgs = RosSharp.RosBridgeClient.MessageTypes.Std;
using UnityEngine.UI;
using System.Threading;

public class TestROS : UnitySubscriber<std_msgs.String>
{
    Text text;
    string lastMessage;
    protected override void Start()
    {
        base.Start();
        text = GetComponent<Text>();
    }

    protected override void ReceiveMessage(std_msgs.String message)
    {
        Debug.Log(message.data);
        lastMessage = message.data;
    }

    void Update()
    {
        if (text)
        {
            text.text = lastMessage;
        }
    }
}
