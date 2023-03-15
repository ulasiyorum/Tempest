using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    public static float weatherTimer;
    public static float degree;
    public static bool windStorm;
    public static float fireDegree;
    public static Vector2 firePosition;
    public static float fireDuration;


    [SerializeField] TMP_Text degreeText;
    public static float Degree
    {
        get => degree + FireDegreeByPosition();
    }

    private static float FireDegreeByPosition()
    {
        float factor = Vector2.Distance(GameManager.i.player.transform.position,firePosition);
        if (factor > 5)
            factor = 0;
        else if (factor < 1)
            factor = 1;
        else
            factor = 1 / factor;

        return fireDegree * factor;

    }


    // Start is called before the first frame update
    void Start()
    {
        fireDuration = 0;
        firePosition = Vector2.zero;
        fireDegree = 0;
        weatherTimer = 0;
        degree = Random.Range(-50, 5);
    }

    // Update is called once per frame
    void Update()
    {
        weatherTimer += Time.deltaTime;
        if(weatherTimer > 40)
        {
            degree = Random.Range(-50, 5);
            if (degree < -30)
            {
                int random = Random.Range(0, 3);
                if(random > 1)
                    windStorm = true;
            } else
            {
                int random = Random.Range(0, 20);
                if (random > 18)
                    windStorm = true;
            }
            weatherTimer = 0;
        }

        if(fireDuration > 0)
        {
            fireDuration -= Time.deltaTime;
        }
        else if(fireDegree > 0)
        {
            fireDegree = 0;
            fireDuration = 0;
            firePosition = Vector2.zero;
        }

        degreeText.text = Degree + " DEGREES";

    }
}
