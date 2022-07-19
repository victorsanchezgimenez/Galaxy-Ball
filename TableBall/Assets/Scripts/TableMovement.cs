using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMovement : MonoBehaviour
{
    [SerializeField] Quaternion originalRotationValue;
    public float maxAngleYAxis = 20f;
    public float maxAngleXAxis = 15f;
    public float speedMovement = 40f;
    public float speedReturn = 0.5f;

    void Start() 
    {
        originalRotationValue = transform.rotation;
    }

    void Update()
    {
        Movement();

    }

    private void Movement()
    {
        Vector3 verticalMovement = Vector3.zero;
        Vector3 horizontalMovement = Vector3.zero;
        float angleX = transform.localEulerAngles.x;
        float angleZ = transform.localEulerAngles.z;
        angleX = (angleX > 180) ? angleX - 360 : angleX;
        angleZ = (angleZ > 180) ? angleZ - 360 : angleZ;

        Debug.Log(Input.acceleration.y);
        if (Input.acceleration.y > 0.1f && angleX < maxAngleYAxis)
        {
            verticalMovement.x = Input.acceleration.y;

            if (verticalMovement.sqrMagnitude > 1)
                verticalMovement.Normalize();

            verticalMovement *= Time.deltaTime;
            transform.Rotate(verticalMovement * speedMovement);


        }
        else if (Input.acceleration.y < -0.1f && angleX > -maxAngleYAxis)
        {
            verticalMovement.x = Input.acceleration.y;
            if (verticalMovement.sqrMagnitude > 1)
                verticalMovement.Normalize();

            verticalMovement *= Time.deltaTime;
            transform.Rotate(verticalMovement * speedMovement);
        }

        //Positive movement
        else if (Input.acceleration.x < -0.1f && angleZ < maxAngleXAxis)
        {
            horizontalMovement.z = Input.acceleration.x;
            if (horizontalMovement.sqrMagnitude > 1)
                horizontalMovement.Normalize();

            horizontalMovement *= Time.deltaTime;
            transform.Rotate(-horizontalMovement * speedMovement);
        }

        //Negative movement
        else if (Input.acceleration.x > 0.1f && angleZ > -maxAngleXAxis)
        {

            horizontalMovement.z = Input.acceleration.x;
            if (horizontalMovement.sqrMagnitude > 1)
                horizontalMovement.Normalize();

            horizontalMovement *= Time.deltaTime;
            transform.Rotate(-horizontalMovement * speedMovement);
        }

        /*else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime * speedReturn);
        }*/
    }
}
