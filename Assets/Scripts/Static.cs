using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Static
{
    //Start Settings
    public static int mapNum = 0;
    public static Vector3 startPos = new Vector3(7.22f, -1.44f, 0f);
    public static float rotateStart;

    public static bool CenterFar = false;
    public static bool LeftFar = false;
    public static bool RightFar = false;
    public static bool CenterClose = false;
    public static bool LeftClose = false;
    public static bool RightClose = false;

    public static int rangeSensor = 2;

    public static float maxSpeed;
    public static float speed;

    public static bool isFinish = false;

    public static bool weNeedTurn = false;
    public static bool isTurn = false;
    public static bool turnDone = false;

    public static bool CenterGarage = false;
    public static bool LeftGarage = false;
    public static bool RightGarage = false;

    public static bool CenterTurn = false;
    public static bool LeftTurn = false;
    public static bool RightTurn = false;
}
