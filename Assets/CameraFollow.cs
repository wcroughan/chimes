using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform followObject;
    [SerializeField]
    float cameraFollowSpeed;
    [SerializeField]
    float cameraRotateSpeed;

    private Vector3 cameraFollowVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, followObject.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;

        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, followObject.rotation, cameraRotateSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }
}
