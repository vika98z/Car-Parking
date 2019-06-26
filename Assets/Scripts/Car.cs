using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool CenterFar, LeftFar, RightFar, CenterClose, LeftClose, RightClose;//        CenterBack, LeftBack, RightBack;   //переменные датчиков
    public Transform AverageLeftFar, AverageRightFar, AverageLeftClose, AverageRightClose,
        CloseLeftFar, CloseRightFar, CloseLeftClose, CloseRightClose, CenterBack, LeftBack, RightBack;
    public GameObject closeGO, averageGO, BackGO; //канвасы, объединяющие ближние, средние и дальние дистанции
    public GameObject Map1, Map2, Map3;
    public float maxSpeed;              //максимальная скорость
    public Transform finishPoit1, finishPoit2, finishPoit3;        //гараж
    public float speed;                 //скорость движения
    public float angle = 30;            //угол
    Rigidbody2D body;                   //машина
    Rigidbody2D finish;                 //гараж (обращаться к этой переменной)
    public Transform left, right, leftC, rightC; //датчики
    public Vector3 rotate;              //угол поворота
    public float xFin, yFin;            //координаты гаража
    public float distance = 0;          //расстояние до гаража
    Vector3 finPos;                     //позиция гаража
    public float distanceFromLeft, distanceFromRight;   //дистанции от левого и правого датчика до гаража
    public bool centerGarage, leftGarage, rightGarage;  //датчики в гараже(в гараже рассматриваем только ближние датчики)

    //Действия, совершаемые единожды при запуске программы
    void Start()
    {
        //Инициализуем переменную машины
        body = GetComponent<Rigidbody2D>();
        //Инициализуем переменную гаража в зависимости от выбранной карты
        switch (Static.mapNum)
        {
            case 1:
                finish = finishPoit1.GetComponent<Rigidbody2D>();
                Map1.gameObject.SetActive(true);
                Map2.gameObject.SetActive(false);
                Map3.gameObject.SetActive(false);
                break;
            case 2:
                finish = finishPoit2.GetComponent<Rigidbody2D>();
                Map1.gameObject.SetActive(false);
                Map2.gameObject.SetActive(true);
                Map3.gameObject.SetActive(false);
                break;
            case 3:
                finish = finishPoit3.GetComponent<Rigidbody2D>();
                Map1.gameObject.SetActive(false);
                Map2.gameObject.SetActive(false);
                Map3.gameObject.SetActive(true);
                break;
        }
        //Находим координаты въезда в гараж
        yFin = finish.position.y - finish.GetComponent<BoxCollider2D>().bounds.size.y / 2f;
        xFin = finish.position.x;
        finPos = new Vector3(xFin, yFin, 0);

        body.transform.Translate(Static.startPos - body.transform.position);

        Static.CenterFar = false;
        Static.LeftFar = false;
        Static.RightFar = false;
        Static.CenterClose = false;
        Static.LeftClose = false;
        Static.RightClose = false;
        Static.speed = Static.maxSpeed;
        Static.isFinish = false;
        Static.CenterGarage = false;
        Static.LeftGarage = false;
        Static.RightGarage = false;
        Static.isTurn = false;
        Static.turnDone = false;

        if (Static.rangeSensor == 1)
        {
            left = CloseLeftFar;
            right = CloseRightFar;
            leftC = CloseLeftClose;
            rightC = CloseRightClose;
            closeGO.gameObject.SetActive(true);
            averageGO.gameObject.SetActive(false);
        }
        else
        {
            left = AverageLeftFar;
            right = AverageRightFar;
            leftC = AverageLeftClose;
            rightC = AverageRightClose;
            closeGO.gameObject.SetActive(false);
            averageGO.gameObject.SetActive(true);
        }

        if (body.rotation != Static.rotateStart)
            body.transform.Rotate(Vector3.forward * Static.rotateStart);
    }

    //Расстояние от точки до гаража
    float Distance(Vector3 point)
    {
        return Vector3.Distance(point, finPos);
    }


    //Какие-либо действия, повторяющиеся постоянно (например, движение)
    void FixedUpdate()
    {
        //Нужно ли разворачиваться задом к гаражу
        if (Static.weNeedTurn && body.position.x <= finish.position.x + 0.05 && body.position.x >= finish.position.x - 0.05 && !Static.turnDone)
        {
            Static.isTurn = true;
        }
        //Перезаписываем переменные, которые могут меняться от кадра к кадру
        centerGarage = Static.CenterGarage;
        leftGarage = Static.LeftGarage;
        rightGarage = Static.RightGarage;
        distanceFromLeft = Distance(left.position);
        distanceFromRight = Distance(right.position);
        //Если разворачиваемся, переходим на задние сенсоры
        if (Static.isTurn)
        {
            left = LeftBack;
            right = RightBack;
            leftC = LeftBack;
            rightC = RightBack;
            BackGO.gameObject.SetActive(true);
            closeGO.gameObject.SetActive(false);
            averageGO.gameObject.SetActive(false);
        }
        //Перезаписываем переменные, которые могут меняться от кадра к кадру
        CenterFar = Static.CenterFar;
        LeftFar = Static.LeftFar;
        RightFar = Static.RightFar;
        CenterClose = Static.CenterClose;
        LeftClose = Static.LeftClose;
        RightClose = Static.RightClose;
        maxSpeed = Static.maxSpeed;
        //Если мы еще не в гараже и не нужно разворачиваться
        if (!Static.isFinish && !Static.isTurn)
        {
            //Если далеко впереди и по бокам все свободно||далеко слева/справа преграда, то быстро едем вперед
            if ((!CenterFar && !LeftFar && !RightFar) || (!CenterFar && !LeftFar && RightFar)
                || (!CenterFar && LeftFar && !RightFar) || (!CenterFar && LeftFar && RightFar))
            {
                speed = maxSpeed;
                rotate = Vector3.zero;
            }
            
            //Если далеко впереди и справа преграда, а слева свободно, то поворачиваем налево
            if (CenterFar && !LeftFar && RightFar)
            {
                rotate = Vector3.back;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
            }
            //Если далеко впереди и слева преграда, а справа свободно, то поворачиваем направо
            if (CenterFar && LeftFar && !RightFar)
            {
                rotate = Vector3.forward;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
            }
            //Если далеко впереди преграда, а по сторонам свободно, то поворачиваем в сторону, дистанция от которой до гаража наименьшая
            if ((CenterFar && !LeftFar && !RightFar) || (CenterFar && !LeftClose && !RightClose))
            {
                //Если расстояние от левого датчика до гаража меньше, то поворачиваем налево, иначе направо
                if ((Distance(left.position) < Distance(right.position)) || (Distance(leftC.position) < Distance(rightC.position)))
                    rotate = Vector3.forward;
                else
                    rotate = Vector3.back;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
            }
            //Если далеко спереди и по сторонам все перекрыто, то рассматриваем территорию ближе, снизив скорость
            if (CenterFar && LeftFar && RightFar)
            {
                speed = maxSpeed - maxSpeed / 2;
                if (!LeftClose)
                {
                    rotate = Vector3.back;
                    body.transform.Rotate(rotate * angle * Time.deltaTime);
                }
                else
                    if (RightClose)
                {
                    rotate = Vector3.forward;
                    body.transform.Rotate(rotate * angle * Time.deltaTime);
                }
            }
            //Если слева и далеко и близко преграда, то сворачиваем направо
            if (LeftFar && LeftClose)
            {
                rotate = Vector3.back;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
            }
            //Если справа и далеко и близко преграда, то сворачиваем налево
            if (RightFar && RightClose)
            {
                rotate = Vector3.forward;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
            }
            //Если спереди далеко и близко преграда, то просматриваем ближние стороны и сворачиваем в свободную
            if (CenterClose && CenterFar)
            {
                if (!LeftClose)
                {
                    rotate = Vector3.back;
                    body.transform.Rotate(rotate * angle * Time.deltaTime);
                }
                else
                    if (RightClose)
                {
                    rotate = Vector3.forward;
                    body.transform.Rotate(rotate * angle * Time.deltaTime);
                }
            }
            body.transform.Translate(new Vector3(-0.01f, 0, 0) * speed * Time.deltaTime);
            body.transform.Rotate(rotate * angle * Time.deltaTime);
        }
        //Если мы в гараже
        else if (Static.isFinish)
        {
            speed = Static.maxSpeed;
            //Если координаты нужные - останавливаемся
            if (body.position.y <= (finish.position.y + Mathf.Sign(finish.position.y) * 0.9) && /////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                body.position.y >= (finish.position.y + Mathf.Sign(finish.position.y) * 0.2))
            {
                Static.maxSpeed = 0;
            }
            //Если передний датчик в гараже, а боковые нет, то едем прямо
            if (Static.CenterGarage && !Static.LeftGarage && !Static.RightGarage)
            {
                rotate = Vector3.zero;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
                body.transform.Translate(new Vector3(-0.01f, 0, 0) * speed * Time.deltaTime);
            }
            //Если передний и левый в гараже, а правый нет, то сворачиваем левее
            if (Static.CenterGarage && Static.LeftGarage && !Static.RightGarage)
            {
                rotate = Vector3.forward;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
                body.transform.Translate(new Vector3(-0.01f, 0, 0) * speed * Time.deltaTime);
            }
            //Если передний и правый в гараже, а левый нет, то сворачиваем правее
            if (Static.CenterGarage && !Static.LeftGarage && Static.RightGarage)
            {
                rotate = Vector3.back;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
                body.transform.Translate(new Vector3(-0.01f, 0, 0) * speed * Time.deltaTime);
            }
            //Если в гараже только правый датчик, то сворачиваем правее
            if (!Static.CenterGarage && !Static.LeftGarage && Static.RightGarage)
            {
                rotate = Vector3.back;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
                body.transform.Translate(new Vector3(-0.01f, 0, 0) * speed * Time.deltaTime);
            }
            //Если в гараже только левый датчик, то сворачиваем левее
            if (!Static.CenterGarage && Static.LeftGarage && !Static.RightGarage)
            {
                rotate = Vector3.forward;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
                body.transform.Translate(new Vector3(-0.01f, 0, 0) * speed * Time.deltaTime);
            }
            //Если все датчики в гараже, то едем вперед
            if (Static.CenterGarage && Static.LeftGarage && Static.RightGarage)
            {
                rotate = Vector3.zero;
                body.transform.Rotate(rotate * angle * Time.deltaTime);
                body.transform.Translate(new Vector3(-0.01f, 0, 0) * speed * Time.deltaTime);
            }
            body.transform.Translate(new Vector3(-0.01f, 0, 0) * speed * Time.deltaTime);
            body.transform.Rotate(rotate * angle * Time.deltaTime);
        }
        //Если мы перед гаражом, то разворачиваемся, чтобы заехать задом
        else if (Static.isTurn)
        {
            speed = -20;
            if (!Static.CenterFar && !Static.LeftFar && !Static.RightFar)
            {
                if (Distance(leftC.position) > Distance(rightC.position))
                {
                    if (!(Distance(leftC.position) > Distance(rightC.position) - 0.1 && Distance(leftC.position) < Distance(rightC.position) + 0.1 && 
                        Distance(body.position) > Distance(rightC.position)))
                    {
                        rotate = Vector3.back;
                        body.transform.Rotate(rotate * angle * Time.deltaTime);
                        body.transform.Translate(new Vector3(-0.01f, 0, 0) * speed * Time.deltaTime);
                    }
                }
                else
                {
                    if (!(Distance(leftC.position) > Distance(rightC.position) - 0.1 && Distance(leftC.position) < Distance(rightC.position) + 0.1 &&
                        Distance(body.position) > Distance(rightC.position)))
                    {

                        rotate = Vector3.forward;
                        body.transform.Rotate(rotate * angle * Time.deltaTime);
                        body.transform.Translate(new Vector3(-0.01f, 0, 0) * speed * Time.deltaTime);
                    }
                }
            }
            if (Distance(leftC.position) > Distance(rightC.position) - 0.1 && Distance(leftC.position) < Distance(rightC.position) + 0.1)
            {
                Static.isTurn = false;
                Static.turnDone = true;
                Static.maxSpeed = -Static.maxSpeed;
            }

        }
    }
}