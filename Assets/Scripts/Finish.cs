using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    Rigidbody2D finish;

    void Start()
    {
        finish = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject enteredObject = col.gameObject;
        
        if (enteredObject.name == "CenterClose" || enteredObject.name == "CenterBack")
        {
            Static.isFinish = true;
            Static.CenterGarage = true;
        }
        if (enteredObject.name == "LeftClose" || enteredObject.name == "LeftBack")
        {
            Static.isFinish = true;
            Static.LeftGarage = true;
        }
        if (enteredObject.name == "RightClose" || enteredObject.name == "RightBack")
        {
            Static.isFinish = true;
            Static.RightGarage = true;
        }
        if (Static.maxSpeed < 0)
            Static.speed = Static.maxSpeed = -21;
        else
            Static.speed = Static.maxSpeed = 21;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        GameObject enteredObject = col.gameObject;
        if (enteredObject.name == "CenterClose" || enteredObject.name == "CenterBack")
            Static.CenterGarage = false;
        else
            if (enteredObject.name == "LeftClose" || enteredObject.name == "LeftBack")
            Static.LeftGarage = false;
        else
            if (enteredObject.name == "RightClose" || enteredObject.name == "RightBack")
            Static.RightGarage = false;
        if (!Static.CenterGarage && !Static.LeftGarage && !Static.RightGarage)
            Static.isFinish = false;
    }
}
