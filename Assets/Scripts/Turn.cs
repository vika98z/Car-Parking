using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    Rigidbody2D turn;

    void Start()
    {
        turn = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject enteredObject = col.gameObject;

        if (enteredObject.name == "CenterClose")
        {
            Static.isTurn = true;
            Static.CenterTurn = true;
            Static.speed = Static.maxSpeed = 20;
        }
        if (enteredObject.name == "LeftClose")
        {
            Static.isTurn = true;
            Static.LeftTurn = true;
            Static.speed = Static.maxSpeed = 20;
        }
        if (enteredObject.name == "RightClose")
        {
            Static.isTurn = true;
            Static.RightTurn = true;
            Static.speed = Static.maxSpeed = 20;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        GameObject enteredObject = col.gameObject;
        if (enteredObject.name == "CenterClose")
            Static.CenterTurn = false;
        else
            if (enteredObject.name == "LeftClose")
            Static.LeftTurn = false;
        else
            if (enteredObject.name == "RightClose")
            Static.RightTurn = false;
        if (!Static.CenterTurn && !Static.LeftTurn && !Static.RightTurn)
            Static.isTurn = false;
    }
}
