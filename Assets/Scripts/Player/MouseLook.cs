using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private float xRotation = 0, angle = 10;
    public Transform left, right;
    public LayerMask wallMask;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mousex = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mousey;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, transform.localRotation.z);
        playerBody.Rotate(Vector3.up * mousex);
        
       /* bool rightCheckSphere = Physics.CheckSphere(right.position, .5f, wallMask);
        bool leftCheckSphere = Physics.CheckSphere(left.position, .5f, wallMask);
        
        if (rightCheckSphere)
        {
            transform.localRotation = Quaternion.Euler(0,0,-angle);
        } else if (leftCheckSphere)
        {
            transform.localRotation = Quaternion.Euler(0,0,angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0);
        }
        */
    }
}
