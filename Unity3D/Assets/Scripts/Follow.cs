using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public Transform follow;

    Vector3 offset;
    Quaternion offAngle;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - follow.position;
        offAngle = transform.rotation * Quaternion.Inverse(follow.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.position + follow.forward * offset.x + follow.up * offset.y + follow.right * offset.z;
        transform.rotation = follow.rotation * offAngle;
    }
}
