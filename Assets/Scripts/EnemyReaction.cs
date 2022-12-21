using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class EnemyReaction : MonoBehaviour
{
    // Use this for initialization
    private Vector2 Home;

    public int count = 0;

    public Camera MainCamera;

    public CinemachineVirtualCamera vcam; //to assign cam

    private Light Light;

    float numVignette;

    private Animator m_Anim; // Reference to the player's animator component.

    public bool m_FacingRight = false; // For determining which way the player is currently facing.

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

    private GameObject[] PickUpQuantity; //Total number of Pickups

    void Start()
    {
        print("Hello World!");

        PickUpQuantity = GameObject.FindGameObjectsWithTag("PickUp");
        print("PickUpQuantity" + PickUpQuantity.Length);
        Home =
            new Vector2(gameObject.transform.position.x,
                gameObject.transform.position.y);

        rb2D = GetComponent<Rigidbody2D>();
        Light = GetComponent<Light>();
        m_Anim = GetComponent<Animator>();
        Light.intensity = 0;
        Light.range = 0.75f;
        print("Light.intensity" + Light.intensity);

        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -200 || Input.GetKeyDown("r"))
        {
            TeleportHome (Home); //If player falls out of the plane or R, TP -> COORDS HOME
        }
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        onGround = floorCollider.IsTouching(floorFilter);

        //print("JustJumped:"+justJumped + " - onGround:"+onGround);
        if (
            !justJumped &&
            Input.GetKeyDown(KeyCode.Space) &&
            onGround &&
            rb2D.velocity.y == 0
        )
        {
            justJumped = true;
        }
    }

    void FixedUpdate()
    {
        rb2D.velocity =
            new Vector2(horizontalMovement * movementSpeed, rb2D.velocity.y);

        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        //m_Anim.SetFloat("Speed", Mathf.Abs(h));
        if (justJumped)
        {
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            //m_Anim.SetBool("Ground", false);
            justJumped = false;
            if (rb2D.velocity.x == 0 && rb2D.velocity.y != 0 && onGround)
            {
                justJumped = false;
            }
        }

        //m_Anim.SetBool("Ground", onGround);
        // Set the vertical animation
        //m_Anim.SetFloat("vSpeed", rb2D.velocity.y);
        // If the input (KEYBOARD) is moving the player and the player is facing the other direction->FLIP
        if ((h > 0 && !m_FacingRight) || h < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    void TeleportHome(Vector2 Home)
    {
        rb2D.velocity = Vector2.zero; //Freeze character
        transform.position = Home; //TP Sphere -> Home
    }

    // This function is called every time another collider overlaps the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Checking if the overlapped collider is an enemy
            print("ENEMY");
            //TeleportHome(Home);
        }
        else if (other.CompareTag("PickUp"))
        {
            //Check if Collider has Tag "Pickup"
            other.gameObject.SetActive(false); //Hides Pickup
            PickUpLight (count);
            MainCamera.GetComponent<CameraController>().WhenPickUp(count);

            count = count + 1;
            print("count:" + count);

            if (count % 5 == 0)
            {
                //If count quantity is 5/10/15/20...
                Home = other.transform.position;
                print("Spawn point Updated to (" +
                other.transform.position +
                ")!");
            }
        }
        else if (other.CompareTag("Finish"))
        {
            if (count >= 20)
            {
                //IF all Pickups hasn't been picked up yet
                float num = 1f;
                Light.intensity = 0.05f;
                Light.range = 0.05f;
                MainCamera
                    .GetComponent<CameraController>()
                    .ModifyVignette(num, false);
                vcam.m_Lens.OrthographicSize = 6;

                //to access cam, Lens and then OrthograhicSize
                print("Enter House");
                SceneManager
                    .LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Next Level
            }
        }
    }

    void PickUpLight(int count)
    {
        if (count < 5)
        {
            Light.intensity = Light.intensity + (0.015f * count);

            //print("Light.intensity"+Light.intensity);
            Light.range = Light.range + (0.06f * count);
            //print("Light.range"+Light.range);
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
