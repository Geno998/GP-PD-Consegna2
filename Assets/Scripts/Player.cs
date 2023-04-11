using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{


    [SerializeField] int speed;


    Vector2 movement;




    bool moveHor;
    bool moveVert;

    Rigidbody2D rb;


    bool Placed;

    [SerializeField] LayerMask empty;
    [SerializeField] GameObject bomb;
    [SerializeField] GameObject cameraPos;

    [SerializeField] Animator animBody;
    [SerializeField] Animator animArm;
    [SerializeField] Animator animBomb;

    [SerializeField] GameObject bombSprite;
    [SerializeField] GameObject armSprite;

    bool moving;
    bool horizontal;
    float dir;

    sceneManager sM;
    UiManager uM;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sM = GameManager.Instance.SM;
        uM = GameManager.Instance.UM;
    }


    private void Update()
    {
        move();
        anim();
        Placebomb();
        moveCam();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sM.loadManiMenu();
        }
    }




    private void move()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * speed;

        if (movement.x == 0)
        {
            movement.y = Input.GetAxisRaw("Vertical") * speed;
        }
        else
        {
            movement.y = 0;
            horizontal = true;
        }





        dir = movement.y * 2;
        rb.velocity = movement;

    }

    void anim()
    {
        animArm.SetBool("moving", moving);
        animBomb.SetBool("moving", moving);
        animBody.SetBool("moving", moving);

        animArm.SetBool("horizlontal", horizontal);
        animBomb.SetBool("horizlontal", horizontal);
        animBody.SetBool("horizlontal", horizontal);


        animArm.SetFloat("upDown", dir);
        animBomb.SetFloat("upDown", dir);
        animBody.SetFloat("upDown", dir);

        if (dir > 0)
        {
            bombSprite.SetActive(false);
        }
        else if (dir < 0)
        {
            bombSprite.SetActive(true);
        }


        if (dir != 0)
        {
            horizontal = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (horizontal)
        {
            armSprite.SetActive(true);
            bombSprite.SetActive(true);

        }
        else
        {
            armSprite.SetActive(false);
        }


        if(movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else if (movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        if (movement.x != 0 || movement.y != 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Placed)
        {
            Instantiate(bomb, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Placed = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boom") || collision.CompareTag("Enemy"))
        {
            
            PlayerPrefs.SetInt("win", 0);
            PlayerPrefs.SetInt("score", uM.points);
            sM.loadEndScreen();
        }
    }


    void Placebomb()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Placed = true;
        }

    }


    void moveCam()
    {
        if (transform.position.x >= 0.5f && transform.position.x <= 17.5f)
        {
            cameraPos.transform.position = new Vector3(transform.position.x, 0.5f, cameraPos.transform.position.z);
        }
    }

}
