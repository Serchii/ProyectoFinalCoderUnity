using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    Rigidbody rb;

    Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>(); 
    }

    void Update()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x,transform.position.y,pointToLook.z));
        }
    }
}
