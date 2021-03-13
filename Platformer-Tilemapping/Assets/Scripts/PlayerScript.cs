using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    private int scoreValue = 0;
    private LifeManager lifeSystem;
    private AudioController musicController;
    private bool facingRight = true;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        lifeSystem = FindObjectOfType<LifeManager>();
        musicController = FindObjectOfType<AudioController>();
        anim = GetComponent<Animator>();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (isOnGround == false)
        {
            anim.SetInteger("State", 2);
        }
    }

    void Update()
    {
       if (((Input.GetKey(KeyCode.D))||(Input.GetKey(KeyCode.A))) && isOnGround)
            {
                anim.SetInteger("State", 1);
            }
     
            else if (isOnGround)
            {
                anim.SetInteger("State", 0);
            }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue >= 8)
            {
                winText.text = "You win! Game created by Michael Harris.";
                musicController.WinMusic();

            }

            if (scoreValue == 4)
            {
                transform.position = new Vector3(42.91f, -16.51f, -7f);
                lifeSystem.GiveLife();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            lifeSystem.TakeLife();
            Destroy(collision.collider.gameObject);
        }
    }
}
