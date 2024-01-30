using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraControl : MonoBehaviour
{
   
    public Transform cameraTarget;
    public Vector3 offset;

    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - cameraTarget.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = new Vector3 (0,0,offset.z + cameraTarget.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, 10 * Time.deltaTime);
    }
}
