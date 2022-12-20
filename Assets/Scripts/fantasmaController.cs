using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class fantasmaController : MonoBehaviour
{
    //variables publicas
	public float velocidadObjeto = 0;
	public float distanciaRecorrer = 0;
	public int tiempoPerseguir = 0;
	public GameObject jugador;

    //variables privadas
	private float maxDistanciaRecorrer = 0;
	private float minDistanciaRecorrer = 0;
	private bool jugadorDetectado = false;
	private float opacidad = 1f;
	private Vector3 posicionOriginal;
	private DateTime contador;

	[Header("aaaaaaa")]
	public float attackSpeed = 2;
	public float attackDistance;
	public float bufferDistance;
	public GameObject player;
	Transform playerTransform;

    //se ejecuta al principio de la escena
	void Start() {
		GetPlayerTransform();
        //inicializamos las variables
		maxDistanciaRecorrer = transform.position.x + distanciaRecorrer;
		minDistanciaRecorrer = transform.position.x - distanciaRecorrer;
		contador = System.DateTime.Now;
		posicionOriginal = transform.position;
		//Collider c = child.gameObject.GetComponent<Collider>();    
	}

    //se ejecuta cada frame
	void Update() {
        //nos aseguramos que no se inclina
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
		print("jugadorDetectado" + jugadorDetectado);
        //si el jugador no ha sido detectado
		/*var distance = Vector3.Distance(playerTransform.position, transform.position);
        // Debug.Log("Distance to Player" + distance);
		print("distance: " + distance + " - AttackDistance" + attackDistance + " - bufferDistance: " + bufferDistance);
		
		if (distance > attackDistance && distance < bufferDistance) //if player is bertween attackDistance and bufferDistance
		{
			print("transform.position fantasama moverses: ");

			if (transform.position.x > playerTransform.position.x){ //if player is left side of enemy (20 (enemy) > -10 (player))
				print("transform.position.x : "+transform.position.x + " -- playerTransform.position.x:" +playerTransform.position.x );
				
				transform.position = new Vector3(transform.position.x + (playerTransform.position.x /4) * Time.deltaTime, transform.position.y + (playerTransform.position.y / 10 ) * Time.deltaTime,transform.position.z); //(vect3(20 - (10/100),y - (player.y/100)))
						 						// (-2 + (-15/2)*time)
				//transform.position =  ((transform.position.x + (-1) * attackSpeed * Time.deltaTime), transform.position,0f);
			} else {
				transform.position = new Vector3(transform.position.x - (playerTransform.position.x /4) * Time.deltaTime, transform.position.y + (playerTransform.position.y / 10 ) * Time.deltaTime,transform.position.z); //(vect3(20 - (10/100),y - (player.y/100)))
				//transform.position =  ((transform.position.x + 1 * attackSpeed * Time.deltaTime), transform.position.y,0);
			}
		} else {//si la opacidad es menor a 1f, la cambiamos
			/*if (opacidad < 1f){
				opacidad = 1f;
				ChangeAlpha(this.GetComponent<Renderer>().material, opacidad);
			}*/

            //movemos al objeto en bucle
			transform.position = new Vector3(Mathf.PingPong(Time.time * velocidadObjeto, maxDistanciaRecorrer-minDistanciaRecorrer) + minDistanciaRecorrer, transform.position.y, 0);
		}
				/*if (!jugadorDetectado){
					
        //si el jugador ha sido detectado
					else {
            //si no ha pasado el tiempo de seguir
						if ((DateTime.Now - contador).TotalSeconds < tiempoPerseguir){
							print("no ha pasado el tiempod e seguir");


                /*float x = 0;
                float y = Input.GetAxis("Horizontal") * velocidadObjeto * Time.deltaTime;
                float z = 0;

                x += velocidadObjeto * Time.deltaTime;

                //movemos al objeto
                transform.position += new Vector3(x, y, z);*/
           /* }
            //si ha pasado el tiempo de seguir
            else {
            	opacidad = opacidad - 0.1f;

                //cambiar la opacidad
            	ChangeAlpha(this.GetComponent<Renderer>().material, opacidad);

                //si la opacidad es menor a 0
            	if (opacidad < 0f){
            		jugadorDetectado = false;

                    //mandamos al objeto a la posicion original
            		transform.position = posicionOriginal;
            	}
            }  
            }*/
        


        void GetPlayerTransform()
        {
        	if (player != null)
        	{
        		playerTransform = player.transform;
        	}
        	else
        	{
        		Debug.Log("Player not specified in Inspector");
        	}
        }



    //https://stackoverflow.com/a/62022162
        void ChangeAlpha(Material mat, float alphaVal) {
        	Color oldColor = mat.color;
        	Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        	mat.SetColor("_Color", newColor);
        }

        void OnTriggerEnter2D(Collider2D other) {
        //si es el jugador
        	if (other.tag == "Player"){

        		jugadorDetectado = true;

            //si no han passat més temps del establert
        		if ((DateTime.Now - contador).TotalSeconds > tiempoPerseguir){
        			contador = System.DateTime.Now;
        		}
        	}

        //si es la luz
        //if (other.gameObject.name == "Luz"){
        	if (other.tag == "Luz"){

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
        }
    }
