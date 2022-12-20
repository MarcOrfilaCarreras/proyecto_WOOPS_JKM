using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fantasmaController : MonoBehaviour
{
    //variables publicas
    public float velocidadObjeto = 0;

    public float distanciaRecorrer = 0;

    public int tiempoPerseguir = 0;

    public GameObject jugador;

    Rigidbody2D enemyRigidBody2D;

    //variables privadas
    private float maxDistanciaRecorrer = 0;

    private float minDistanciaRecorrer = 0;

    private bool jugadorDetectado = false;

    private float opacidad = 1f;

    private Vector3 posicionOriginal;

    private DateTime contador;

    Transform playerTransform;

    private bool _isFacingLeft;

    private bool _moveRight = true;

    Vector3 InitialPos;

    private float _startPos;

    private float _endPos;

    //se ejecuta al principio de la escena
    void Start()
    {
        InitialPos =
            new Vector3(transform.position.x,
                transform.position.y,
                transform.position.z);
        GetPlayerTransform();

        //
        //inicializamos las variables
        maxDistanciaRecorrer = transform.position.x + distanciaRecorrer;
        minDistanciaRecorrer = transform.position.x - distanciaRecorrer;
        contador = System.DateTime.Now;
        posicionOriginal = transform.position;

        //FLIPPIN TURTLES
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        _startPos = maxDistanciaRecorrer;
        _endPos = minDistanciaRecorrer;
        _isFacingLeft = transform.localScale.x > 0;
    }

    //se ejecuta cada frame
    void Update()
    {
        //nos aseguramos que no se inclina
        transform.rotation =
            Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        print("jugadorDetectado" + jugadorDetectado);

        print(minDistanciaRecorrer +
        "--" +
        transform.position.x +
        "--" +
        maxDistanciaRecorrer);

        if (
            ((transform.position.x+0.2) >= maxDistanciaRecorrer && _moveRight) ||
            ((transform.position.x-0.2) <= minDistanciaRecorrer && !_moveRight) 
        )
        {
            Flip();
			_moveRight = !_moveRight;
        }
        //movemos al objeto en bucle
        transform.position =
            new Vector3(Mathf
                    .PingPong(Time.time * velocidadObjeto,
                    maxDistanciaRecorrer - minDistanciaRecorrer) +
                minDistanciaRecorrer,
                InitialPos.y,
                0);
    }

    void GetPlayerTransform()
    {
        if (jugador != null)
        {
            playerTransform = jugador.transform;
        }
        else
        {
            Debug.Log("Player not specified in Inspector");
        }
    }

    //https://stackoverflow.com/a/62022162
    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor =
            new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //si es el jugador
        if (other.tag == "Player")
        {
            jugadorDetectado = true;

            //si no han passat més temps del establert
            if ((DateTime.Now - contador).TotalSeconds > tiempoPerseguir)
            {
                contador = System.DateTime.Now;
            }
        }

        //si es la luz
        //if (other.gameObject.name == "Luz"){
        if (other.tag == "Luz")
        {
            //ocultamos
            ChangeAlpha(this.GetComponent<Renderer>().material, 0f);

            //destruimos
            Destroy(this);
        }
    }

    private void Flip()
    {
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        print("Flip");
    }
}
