using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLAndR : MonoBehaviour
{

    public float speedMove = 5f;
    private int limitMovement = 3;
    private Vector3 startPositionX;
    private bool minLimitPosition;
    private bool maxLimitPosition;

    private void Start()
    {
        startPositionX = transform.position;
    }

    private void Update()
    {
        //newPositionX.x = startPositionX.x + (limitMovement * Mathf.Sin(Time.time * speedMove));
        //transform.Translate(newPositionX* Time.deltaTime); 

        MoveObjectPingPong();
        
    }

    /// <summary>
    /// Encargado de mover el objeto de izq a derecha (o viceversa) dependiendo de su punto de origen
    /// </summary>
    private void MoveObjectPingPong()
    {
        //Verifica donde se posisiona originalmente el objeto, menor a 0 o mayor a 0
        if (startPositionX.x > 0 && !maxLimitPosition)
        {
            transform.Translate(Vector3.right * speedMove * Time.deltaTime);
            if (transform.position.x >= limitMovement)
            {
                maxLimitPosition = true;
                minLimitPosition = false;
            }
        }
        else if (startPositionX.x < 0 && !minLimitPosition)
        {
            transform.Translate(Vector3.left * speedMove * Time.deltaTime);
            if (transform.position.x <= -limitMovement)
            {
                minLimitPosition = true;
                maxLimitPosition = false;
            }
        }

        //Una vez que se comprueba la posicion inicial y se llega a un limite en x se empezara a ejecutar estos fragmentos de codigos condicionales
        if (maxLimitPosition)
        {
            transform.Translate(Vector3.left * speedMove * Time.deltaTime);
            if (transform.position.x <= -limitMovement)
            {
                minLimitPosition = true;
                maxLimitPosition = false;
            }
        }
        if (minLimitPosition)
        {
            transform.Translate(Vector3.right * speedMove * Time.deltaTime);
            if (transform.position.x >= limitMovement)
            {
                maxLimitPosition = true;
                minLimitPosition = false;
            }
        }
    }

}