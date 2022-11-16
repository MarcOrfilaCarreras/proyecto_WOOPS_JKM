using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restar : MonoBehaviour
{
    public Transform RestarPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = RestarPoint.position;
        }
    }
}
