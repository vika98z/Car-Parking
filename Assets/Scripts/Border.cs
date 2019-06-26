using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    Rigidbody2D border;
    // Start is called before the first frame update
    void Start()
    {
        border = GetComponent<Rigidbody2D>();
    }

    //Действия при касании преграды
    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject enteredObject = col.gameObject;
        if (enteredObject.name == "CenterFar")
            Static.CenterFar = true;
        else
            if (enteredObject.name == "LeftFar")
            Static.LeftFar = true;
        else
            if (enteredObject.name == "RightFar")
            Static.RightFar = true;
        else
            if (enteredObject.name == "CenterClose")
            Static.CenterClose = true;
        else
            if (enteredObject.name == "LeftClose")
            Static.LeftClose = true;
        else
            if (enteredObject.name == "RightClose")
            Static.RightClose = true;
    }

    //Действия при продолжительном касании с преградой
    void OnTriggerStay2D(Collider2D col)
    {
        GameObject enteredObject = col.gameObject;
        if (enteredObject.name == "CenterFar")
            Static.CenterFar = true;
        else
            if (enteredObject.name == "LeftFar")
            Static.LeftFar = true;
        else
            if (enteredObject.name == "RightFar")
            Static.RightFar = true;
        else
            if (enteredObject.name == "CenterClose")
            Static.CenterClose = true;
        else
            if (enteredObject.name == "LeftClose")
            Static.LeftClose = true;
        else
            if (enteredObject.name == "RightClose")
            Static.RightClose = true;
    }

    //Действия при уходе от преграды
    void OnTriggerExit2D(Collider2D col)
    {
        GameObject enteredObject = col.gameObject;
        if (enteredObject.name == "CenterFar")
            Static.CenterFar = false;
        else
            if (enteredObject.name == "LeftFar")
            Static.LeftFar = false;
        else
            if (enteredObject.name == "RightFar")
            Static.RightFar = false;
        else
            if (enteredObject.name == "CenterClose")
            Static.CenterClose = false;
        else
            if (enteredObject.name == "LeftClose")
            Static.LeftClose = false;
        else
            if (enteredObject.name == "RightClose")
            Static.RightClose = false;
    }
}
