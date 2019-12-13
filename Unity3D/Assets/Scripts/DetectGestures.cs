using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectGestures : MonoBehaviour
{
    SenseGlove_FingerDetector detector;
    bool[] fingers;
    string fingerPattern;
    public Text text;

    static Dictionary<string, float> axis = new Dictionary<string, float>();

    // Start is called before the first frame update
    void Start()
    {
        detector = GetComponent<SenseGlove_FingerDetector>();
        fingers = new bool[5];

        axis["forward"] = 0;
        axis["turn"] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        fingerPattern = "";
        for (int i = 0; i < 5; i++)
        {
            fingers[i] = detector.TouchedBy(i);
            fingerPattern += (fingers[i]) ? 1 : 0;

        }

        setText("Nothing");

        if (fingerPattern == "10111")
        {
            axis["forward"] = 1;
            setText("Pointing");
        }
        else
        {
            axis["forward"] = 0;

        }

        if (fingerPattern == "11111")
        {
            Vector3 euler = detector.modelToCheck.wristTransfrom.localEulerAngles;
            if (euler.x > 180)
                euler.x -= 360;
            //Debug.Log(euler);
            if(Mathf.Abs(euler.x) < 10)
            {
                axis["turn"] = 0;
            }
            else
            {  
                axis["turn"] = Mathf.Clamp(euler.x / 100f, -1, 1);
            }
            
            setText("Turning");
        }
        else
        {
            axis["turn"] = 0;

        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            detector.modelToCheck.CalibrateWrist();
        }
        
        

        
    }

    void setText(string message)
    {
        if (text != null)
            text.text = message;
    }

    public static float getAxis(string name)
    {
        return axis[name];
    }
}
