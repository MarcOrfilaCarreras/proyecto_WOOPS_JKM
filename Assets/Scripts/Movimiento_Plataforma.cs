using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Plataforma : MonoBehaviour
{
    public GameObject objectMove;
    public Transform StardPoint;
    public Transform Endpoint;

    public float Velocidad;

    private Vector3 MoverHacia;

    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = Endpoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        objectMove.transform.position = Vector3.MoveTowards(
            objectMove.transform.position,
            MoverHacia,
            Velocidad * Time.deltaTime
        );

        if (objectMove.transform.position == Endpoint.position)
        {
            MoverHacia = StardPoint.position;
        }
        if (objectMove.transform.position == StardPoint.position)
        {
            MoverHacia = Endpoint.position;
        }

        
    }
}
