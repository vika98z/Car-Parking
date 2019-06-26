using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartSettings : MonoBehaviour
{
    public Dropdown map;
    public InputField startPosition;//
    public Dropdown sideWorld;
    public InputField rangeSensor;//
    public InputField maxSpeed;//
    public Dropdown direction;
    
    public void SetSettings()
    {
        //Номер карты
        int index = map.value;
        switch (index)
        {
            case 0:
                Static.mapNum = 1;
                break;
            case 1:
                Static.mapNum = 2;
                break;
            case 2:
                Static.mapNum = 3;
                break;
        }

        //Координаты начального положения
        string[] parts = startPosition.text.Split(new Char[] { '(', ';', ')' });
        Static.startPos = new Vector3((float)(Convert.ToDouble(parts[1])), (float)(Convert.ToDouble(parts[2])), 0f);

        //Сторона света
        int side = sideWorld.value;
        switch (side)
        {
            case 0:
                Static.rotateStart = 0f;
                break;
            case 1:
                Static.rotateStart = 270f;
                break;
            case 2:
                Static.rotateStart = 180f;
                break;
            case 3:
                Static.rotateStart = 90f;
                break;
        }

        //Двльность сенсоров
        if (rangeSensor.text == "1")
            Static.rangeSensor = 1;
        else
            Static.rangeSensor = 2;

        //Максимальная скорость
        Static.maxSpeed = (float)Convert.ToDouble(maxSpeed.text);

        //Направление
        int dir = direction.value;
        switch (dir)
        {
            case 0:
                Static.weNeedTurn = false;
                break;
            case 1:
                Static.weNeedTurn = true;
                break;
        }
    }
}
