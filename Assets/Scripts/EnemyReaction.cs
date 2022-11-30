using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.CrossPlatformInput;

public class EnemyReaction : MonoBehaviour {
  	// Use this for initialization
	private Vector2 Home;
	public int count = 0;
	public Camera MainCamera;
	public CinemachineVirtualCamera vcam; //to assign cam
	private Light Light;
	float numVignette;
	
        private Animator m_Anim;            // Reference to the player's animator component.
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.


        [Header("Movement")]
        public float movementSpeed = 12f;
        private float horizontalMovement;
        private Rigidbody2D rb2D;

        [Header("Jumping")]
        public float jumpForce = 20f;
        private bool justJumped = false;

        [Header("Ground")]
        public bool onGround = false;
        public Collider2D floorCollider;
        public ContactFilter2D floorFilter;
        //private Vector2 touchOrigin = -Vector2.one;//Used to store location of screen touch origin for mobile controls.

        void Start () {
        	Home = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        	print("Hello World!");
        	rb2D = GetComponent<Rigidbody2D>();
        	Light = GetComponent<Light>();
        	m_Anim = GetComponent<Animator>();
        	Light.intensity = 0;
        	Light.range = 0.75f;
        	print("Light.intensity"+Light.intensity);

        	count = 0;
        }
  // Update is called once per frame
        void Update () {

        	if (/*transform.position.y < -30 ||*/ Input.GetKeyDown("r")){ 
    		TeleportHome(Home);//If player falls out of the plane or R, TP -> COORDS HOME
    	}
    	horizontalMovement = Input.GetAxisRaw("Horizontal");

    	onGround = floorCollider.IsTouching(floorFilter);
    	print("JustJumped:"+justJumped + " - onGround:"+onGround);
    	if (!justJumped && Input.GetKeyDown(KeyCode.Space) && onGround && rb2D.velocity.y == 0){
    		justJumped = true;
    	}

    }

    void FixedUpdate(){
    	rb2D.velocity = new Vector2(horizontalMovement * movementSpeed, rb2D.velocity.y);
    	float h = CrossPlatformInputManager.GetAxis("Horizontal");
    	print("H: "+h);
    	m_Anim.SetFloat("Speed", Mathf.Abs(h));

    	if(justJumped)
    	{
    		rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    		m_Anim.SetBool("Ground", false);
    		justJumped = false;
    		if (rb2D.velocity.x == 0 && rb2D.velocity.y != 0 && onGround){
    			justJumped = false;
    		}

    	}
    	m_Anim.SetBool("Ground", onGround);

            // Set the vertical animation
    	m_Anim.SetFloat("vSpeed", rb2D.velocity.y);

    	// If the input is moving the player and the player is facing the other direction -> FLIP
    	print("m_FacingRight: " + m_FacingRight);

    	if ((h > 0 && m_FacingRight) || h < 0 && !m_FacingRight){
    		print("Flip");
    		Flip();
    	}     
    }
    void TeleportHome(Vector3 Home){
    	rb2D.velocity = Vector2.zero; //Freeze character 
		transform.position = Home; //TP Sphere -> Home
	}

  // This function is called every time another collider overlaps the trigger collider
	void OnTriggerEnter2D (Collider2D other){
		if (other.CompareTag ("Enemy")) {    // Checking if the overlapped collider is an enemy
			print("ENEMY");
			if (transform.position.y > (other.transform.position.y+0.6)){
				Destroy(other.gameObject);
			}else{
				TeleportHome(Home);
			}	

    	}else if (other.CompareTag("PickUp")){ //Check if Collider has Tag "Pickup"
    	count++;
    	print("PickUp: "+count);
		    other.gameObject.SetActive (false);//Hides Pickup
		    PickUpLight(count);
		    MainCamera.GetComponent<CameraController>().WhenPickUp(count);
		    if (count % 5 == 0){
		    	SpawnPointCheck(other.transform.position);
		    }
		    }else if (other.CompareTag("Finish")){
		    	float num = 1f;
		    	Light.intensity = 0.05f;
		    	Light.range = 0.05f;
		    	MainCamera.GetComponent<CameraController>().ModifyVignette(num, false);
		    	SpawnPointCheck(other.transform.position);
	    	vcam.m_Lens.OrthographicSize = 6; //to access cam, Lens and then OrthograhicSize

		    	//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//Next Level
	    }	    	

	}
	void SpawnPointCheck(Vector2 position){
		Home = position;
		print("Spawn point Updated to ("+position+")!");
	}

	void PickUpLight(int count){
		if (count < 5){
			Light.intensity = Light.intensity + (0.015f * count);
			print("Light.intensity"+Light.intensity);
			Light.range = Light.range + (0.06f * count);
			print("Light.range"+Light.range);

		}

	}
	
	IEnumerator EnterHouse(){
		Light.intensity = Light.intensity -(0.015f * count);
		Light.range = Light.range - 0.05f;
		MainCamera.GetComponent<CameraController>().ModifyVignette(1f, false);
		yield return new WaitForSeconds(0.25f);
		Light.intensity = Light.intensity -(0.015f * count);
		yield return new WaitForSeconds(0.25f);
		Light.intensity = Light.intensity -(0.015f * count);
		yield return new WaitForSeconds(0.25f);
		Light.intensity = Light.intensity -(0.015f * count);
		yield return new WaitForSeconds(0.25f);

	}

	private void Flip()
	{
            // Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}


