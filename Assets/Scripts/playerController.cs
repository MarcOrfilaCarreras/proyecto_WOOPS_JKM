using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    //variables publicas
    public float speed = 5f;
    public float jump = 0;

    //variables privadas
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;

    private Rigidbody2D rb;

    //se ejecuta al principio de la escena
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (moveLeft == true)
        {
            rb.AddForce((transform.right * speed * Time.fixedDeltaTime * 100f) * -1, ForceMode2D.Force);
        }

        if (moveRight == true)
        {
            rb.AddForce((transform.right * speed * Time.fixedDeltaTime * 100f), ForceMode2D.Force);
        }

        if (moveUp == true)
        {
            rb.AddForce((Vector2.up * jump * Time.fixedDeltaTime * 100f), ForceMode2D.Force);
        }
    }

    public void MoveLeft(bool _move)
    {
        moveLeft = _move; 
    }

    public void MoveRight(bool _move)
    {
        moveRight = _move; 
    }

    public void MoveUp(bool _move)
    {
        moveUp = _move; 
    }
}