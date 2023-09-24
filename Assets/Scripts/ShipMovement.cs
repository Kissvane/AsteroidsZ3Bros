using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private Transform shipTransform;
    [SerializeField] private Rigidbody2D shipRigidbody;
    [SerializeField] private float acceleration;
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0) 
        {
            shipRigidbody.AddForce(
                shipTransform.right * 
                Mathf.Max(Input.GetAxis("Vertical"),0) *
                acceleration * 
                Time.deltaTime, ForceMode2D.Force);
        }

        shipTransform.localEulerAngles += 
            -Vector3.forward * 
            rotationSpeed * 
            Input.GetAxis("Horizontal") * 
            Time.deltaTime;
    }
}
