using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{


    bool Upgrade;

    [SerializeField] bool bomb;
    [SerializeField] bool last;




    [SerializeField] LayerMask mask;


    [SerializeField] GameObject boom;
    [SerializeField] GameObject lastBoom;
    [SerializeField] GameObject startBoom;
    [SerializeField] GameObject empty;


    [DoNotSerialize] public float timer = 3;
    float continueTimer = 0.1f;


    Quaternion upRot = Quaternion.Euler(0, 0, 0);
    Quaternion downRot = Quaternion.Euler(0, 0, 180);
    Quaternion rightRot = Quaternion.Euler(0, 0, 270);
    Quaternion leftRot = Quaternion.Euler(0, 0, 90);


    [SerializeField][Range(0, 1)] float colorRange = 1;
    bool flashing;
    [SerializeField] SpriteRenderer sprite;


    UiManager uI;

    void Start()
    {
        uI = GameManager.Instance.UM;
    }


    void Update()
    {
        if (bomb)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                explode();
            }

            colorRange = Mathf.Abs(Mathf.Cos((2f * Mathf.PI * Time.time) / 1f));


            if (colorRange >= 0.9f && timer <= 1.5f)
            {
                flashing = true;
            }

            if (flashing)
            {
                flash();
            }


        }

        if (!bomb)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime * 6;
            }
            else
            {
                Instantiate(empty, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (!last)
            {
                if (continueTimer >= 0)
                {
                    continueTimer -= Time.deltaTime;
                }
                else
                {
                    continueExplode();
                    continueTimer = 100;
                }
            }
        }
    }



    void explode()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + (Vector3.right * 0.7f), Vector2.right, mask);


        if (hit1.collider.CompareTag("Empty") || hit1.collider.CompareTag("Boom"))
        {
            Instantiate(boom, hit1.transform.position, rightRot);
            Destroy(hit1.collider.gameObject);
            uI.points += 100;
        }

        if (hit1.collider.CompareTag("Brick"))
        {
            Instantiate(lastBoom, hit1.transform.position, rightRot);
            Destroy(hit1.collider.gameObject);
            uI.points += 100;
        }

        if (hit1.collider.CompareTag("Bomb"))
        {
            Bomb bomb = hit1.collider.GetComponent<Bomb>();

            bomb.timer = 0;
        }


        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + (Vector3.left * 0.7f), Vector2.left, mask);


        if (hit2.collider.CompareTag("Empty") || hit2.collider.CompareTag("Boom"))
        {
            Instantiate(boom, hit2.transform.position, leftRot);
            Destroy(hit2.collider.gameObject);
        }

        if (hit2.collider.CompareTag("Brick"))
        {
            Instantiate(lastBoom, hit2.transform.position, leftRot);
            Destroy(hit2.collider.gameObject);
            uI.points += 100;
        }

        if (hit2.collider.CompareTag("Bomb"))
        {
            Bomb bomb = hit2.collider.GetComponent<Bomb>();

            bomb.timer = 0;

        }



        RaycastHit2D hit3 = Physics2D.Raycast(transform.position + (Vector3.up * 0.7f), Vector2.up, mask);


        if (hit3.collider.CompareTag("Empty") || hit3.collider.CompareTag("Boom"))
        {
            Instantiate(boom, hit3.transform.position, upRot);
            Destroy(hit3.collider.gameObject);
        }

        if (hit3.collider.CompareTag("Brick"))
        {
            Instantiate(lastBoom, hit3.transform.position, upRot);
            Destroy(hit3.collider.gameObject);
            uI.points += 100;
        }

        if (hit3.collider.CompareTag("Bomb"))
        {
            Bomb bomb = hit3.collider.GetComponent<Bomb>();

            bomb.timer = 0;

        }



        RaycastHit2D hit4 = Physics2D.Raycast(transform.position + (Vector3.down * 0.7f), Vector2.down, mask);


        if (hit4.collider.CompareTag("Empty") || hit4.collider.CompareTag("Boom"))
        {
            Instantiate(boom, hit4.transform.position, downRot);
            Destroy(hit4.collider.gameObject);
        }

        if (hit4.collider.CompareTag("Brick"))
        {
            Instantiate(lastBoom, hit4.transform.position, downRot);
            Destroy(hit4.collider.gameObject);
            uI.points += 100;
        }

        if (hit4.collider.CompareTag("Bomb"))
        {
            Bomb bomb = hit4.collider.GetComponent<Bomb>();

            bomb.timer = 0;
        }


        Instantiate(startBoom, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    void continueExplode()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.up * 0.7f), transform.up, mask);


        if (hit.collider.CompareTag("Empty") || hit.collider.CompareTag("Boom"))
        {
            Instantiate(lastBoom, hit.transform.position, transform.rotation);
            Destroy(hit.collider.gameObject);
        }

        if (hit.collider.CompareTag("Brick"))
        {
            Instantiate(lastBoom, hit.transform.position, transform.rotation);
            Destroy(hit.collider.gameObject);
            uI.points += 100;

        }

        if (hit.collider.CompareTag("Bomb"))
        {
            Bomb bomb = hit.collider.GetComponent<Bomb>();

            bomb.timer = 0;
        }
    }


    void flash()
    {
        sprite.color = Color.Lerp(Color.red, Color.white, colorRange);
    }

 
}
