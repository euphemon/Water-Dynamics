using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class FloatingBoat : MonoBehaviour
{

    public Transform[] Floaters;
    public float UnderWaterDrag = 3f;
    public float UnderWaterAngularDrag = 1f;
    public float AirDrag = 0f;
    public float AirAngularDrag = 0.05f;
    public float FloatingPower = 15f;
    public float WaterHeight = 0f;

    Rigidbody Rb;
    bool Underwater;
    int FloatersUnderWater;
    
    void Start()
    {
        Rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        FloatersUnderWater = 0;
        for (int i = 0; i < Floaters.Length; i++)
        {
            float difference = Floaters[i].position.y - WaterHeight;
            if (difference < 1)
            {
                Rb.AddForceAtPosition(Vector3.up * FloatingPower * Mathf.Abs(difference), Floaters[i].position, ForceMode.Force);
                FloatersUnderWater += 1;
                if (!Underwater)
                {
                    Underwater = true;
                    SwitchState(true);
                }
            }
        }
        if (Underwater && FloatersUnderWater == 0)
        {
            Underwater = false;
            SwitchState(false);
        }
    }
    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            Rb.drag = UnderWaterDrag;
            Rb.angularDrag = UnderWaterAngularDrag;
        }
        else
        {
            Rb.drag = AirDrag;
            Rb.angularDrag = AirAngularDrag;
        }
    }
}
