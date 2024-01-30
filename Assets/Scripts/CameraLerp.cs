using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraLerp : MonoBehaviour
    
{
    public GameObject followTarget;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public float zDepth, yOffset;
    private Vector3 initialPos;

    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(followTarget.transform.position.x, (followTarget.transform.position.y + yOffset), followTarget.transform.position.z -zDepth), ref velocity, smoothTime);
        
    }
}
