using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using sensor_msgs = RosSharp.RosBridgeClient.MessageTypes.Sensor;
using UnityEngine.UI;
using System.Threading;

public class ReceiveImage : UnitySubscriber<sensor_msgs.CompressedImage>
{
    Text text;
    string lastMessage;
    Image image; 
    protected override void Start()
    {
        base.Start();
        image = GetComponent<Image>();
    }


    void Update()
    {
        if (text)
        {
            text.text = lastMessage;
        }
    }

    protected override void ReceiveMessage(sensor_msgs.CompressedImage message)
    {
        Debug.Log("received image");
        Texture2D tex = new Texture2D(100,100);
        tex.LoadImage(message.data);
        image.sprite = Sprite.Create(tex, new Rect(0, 0, 100, 100), Vector2.zero);
    }
}
