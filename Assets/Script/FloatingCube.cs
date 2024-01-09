using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCube : MonoBehaviour
{

    // Use this for initialization

    //these are the floating parameters
    public float WaterLevel = 1;
    public float FloatHeight = 2;
    public float BounceDamp = 0.01f;
    public Vector3 PostionOffset;

    //these are the internal parameters that are obtained in the script;
    private float ForceFactor;
    private Vector3 actionPoint;
    private Vector3 Uplift;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        PostionOffset = new Vector3((float)Random.Range(0, 30) / 1000, (float)Random.Range(0, 30) / 1000, (float)Random.Range(0, 30) / 1000);

    }



    // Update is called once per frame
    void FixedUpdate()
    {

        //the action point considers the offset of the object in local space
        actionPoint = transform.position + transform.TransformDirection(PostionOffset);

        //the force factor is a value between 0 and 1
        ForceFactor = 1.0f - ((actionPoint.y - WaterLevel) / FloatHeight);

        //only when the force applied is greatter than zero 
        if (ForceFactor > 0f)
        {
            Uplift = -Physics.gravity * (ForceFactor - rb.velocity.y * BounceDamp);

            rb.AddForceAtPosition(Uplift, actionPoint);
        }

    }


}