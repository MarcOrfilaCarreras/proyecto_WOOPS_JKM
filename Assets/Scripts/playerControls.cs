using UnityEngine;

public class playerControls : MonoBehaviour
{
    //variables publicas
    public float speed = 5f;
    public float jump = 0;

    //variables privadas
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;
    public bool isOnGround;

    private Vector2 Home;

    private Rigidbody2D rb;

    //se ejecuta al principio de la escena
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isOnGround = true;
                    Home = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "PlataformaMovi") {
            isOnGround = true;
        }

        if (collision.gameObject.tag == "Enemy"){
            TeleportHome(Home);          
        }

    }


    private void FixedUpdate()
    {
        if (moveLeft == true)
        {
            rb.AddForce((transform.right * speed * Time.fixedDeltaTime * 100f) * -1, ForceMode2D.Force);
        }

        if (moveRight == true)
        {
            rb.AddForce((transform.right * speed * Time.fixedDeltaTime * 100f)* -1, ForceMode2D.Force);
        }

        if (moveUp == true && isOnGround)
        {
                        isOnGround = false;

            rb.AddForce((Vector2.up * jump * Time.fixedDeltaTime * 3.5f* 100f), ForceMode2D.Force);

        }
    }

    public void MoveLeft(bool _move)
    {
        moveLeft = _move;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void MoveRight(bool _move)
    {
        moveRight = _move;
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public void MoveUp(bool _move)
    {
        moveUp = _move;
    }

    void TeleportHome(Vector2 Home){
        rb.velocity = Vector2.zero; //Freeze character 
        transform.position = Home; //TP Sphere -> Home
    }
}

