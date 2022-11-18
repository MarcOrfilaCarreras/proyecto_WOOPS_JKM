using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public Camera MainCamera;

    public CinemachineVirtualCamera vcam; //to assign cam
    private Light Light;
    float numVignette;

        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
                    print("want to jump");

            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if ((moveInput > 0 && m_FacingRight) || moveInput < 0 && !m_FacingRight){
            Flip();
        } 
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

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "PlataformaMovi")
        {
            transform.parent = collisionInfo.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "PlataformaMovi")
        {
            transform.parent = null;
        }
    }
}
