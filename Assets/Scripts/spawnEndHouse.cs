using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEndHouse : MonoBehaviour
{

    public GameObject house;
 
    private int contador;

    // Start is called before the first frame update
    void Start()
    {
        house.gameObject.SetActive(false);
    }

    void Update()
    {
        if (contador == 10)
        {
            house.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            contador++;
            print(contador);
        }
    }
}
