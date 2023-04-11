using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    public enum EnemyType
    {
        type1,
        type2,
    }

    [SerializeField] EnemyType type;


    bool death;

    [SerializeField] float speed;
    [SerializeField] float rotSpeed;


    int moveDistance;
    int dirCheck;

    float waiting;

    Quaternion rot;
    Vector3 moveTo;

    bool choosing;

    UiManager uI;


    private void Start()
    {
        choosing = true;
        uI = GameManager.Instance.UM;
    }

    private void Update()
    {
        if (type == EnemyType.type1)
        {
            move1();
        }
        else if (type == EnemyType.type2)
        {
            move2();
        }

        chooseDir();
        KillEnemy();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boom"))
        {
            death = true;
        }
    }




    void move1()
    {
        if (waiting > 0)
        {
            waiting -= Time.deltaTime;
        }
        else
        {
            if (transform.rotation != rot)
            {
                float step = rotSpeed * Time.deltaTime;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, step);
            }
            else
            {
                if (moveDistance > 0)
                {
                    RaycastHit2D hitCheck;

                    hitCheck = Physics2D.Raycast(transform.position + transform.up * 0.7f, transform.up);

                    if (transform.position == moveTo)
                    {
                        if (hitCheck.collider.CompareTag("Empty") || hitCheck.collider.CompareTag("Boom") || hitCheck.collider.CompareTag("Bomb"))
                        {
                            moveDistance--;
                            moveTo = transform.position + transform.up;
                        }
                        else
                        {
                            moveDistance = 0;
                        }
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime * speed);
                    }



                }
                else
                {
                    choosing = true;
                }
            }
        }
    }



    void move2()
    {
        if (waiting > 0)
        {
            waiting -= Time.deltaTime;
        }
        else
        {
            if (transform.rotation != rot)
            {
                float step = rotSpeed * Time.deltaTime;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, step);
            }
            else
            {
                if (moveDistance > 0)
                {
                    RaycastHit2D hitCheck;

                    hitCheck = Physics2D.Raycast(transform.position + transform.up * 0.7f, transform.up);

                    if (transform.position == moveTo)
                    {
                        if (hitCheck.collider.CompareTag("Empty") || hitCheck.collider.CompareTag("Boom") || hitCheck.collider.CompareTag("Bomb"))
                        {
                            moveTo = transform.position + transform.up;
                        }
                        else
                        {
                            moveDistance = 0;
                        }
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime * speed);
                    }
                }
                else
                {
                    choosing = true;
                }
            }
        }
    }





    void chooseDir()
    {
        RaycastHit2D hitDir;

        while (choosing)
        {
            dirCheck = Random.Range(1, 6);

            if (dirCheck == 1)
            {
                hitDir = Physics2D.Raycast(transform.position + Vector3.up * 0.7f, Vector2.up);
                if (hitDir.collider.CompareTag("Empty") || hitDir.collider.CompareTag("Boom") || hitDir.collider.CompareTag("Bomb"))
                {

                    moveDistance = Random.Range(1, 4);
                    moveTo = transform.position + Vector3.up;
                    rot = Quaternion.Euler(0, 0, 0);
                    choosing = false;
                }
            }
            else if (dirCheck == 2)
            {
                hitDir = Physics2D.Raycast(transform.position + Vector3.down * 0.7f, Vector2.down);
                if (hitDir.collider.CompareTag("Empty") || hitDir.collider.CompareTag("Boom") || hitDir.collider.CompareTag("Bomb"))
                {

                    moveDistance = Random.Range(1, 4);
                    moveTo = transform.position + Vector3.down;
                    rot = Quaternion.Euler(0, 0, 180);
                    choosing = false;
                }
            }
            else if (dirCheck == 3)
            {
                hitDir = Physics2D.Raycast(transform.position + Vector3.right * 0.7f, Vector2.right);
                if (hitDir.collider.CompareTag("Empty") || hitDir.collider.CompareTag("Boom") || hitDir.collider.CompareTag("Bomb"))
                {

                    moveDistance = Random.Range(1, 4);
                    moveTo = transform.position + Vector3.right;
                    rot = Quaternion.Euler(0, 0, 270);
                    choosing = false;
                }
            }
            else if (dirCheck == 4)
            {
                hitDir = Physics2D.Raycast(transform.position + Vector3.left * 0.7f, Vector2.left);
                if (hitDir.collider.CompareTag("Empty") || hitDir.collider.CompareTag("Boom") || hitDir.collider.CompareTag("Bomb"))
                {

                    moveDistance = Random.Range(1, 4);
                    moveTo = transform.position + Vector3.left;
                    rot = Quaternion.Euler(0, 0, 90);
                    choosing = false;
                }
            }
            else if (dirCheck == 5)
            {
                moveDistance = 0;
                moveTo = transform.position;
                rot = transform.rotation;
                waiting = Random.Range(0.5f, 2);
                choosing = false;
            }
        }
    }


    void KillEnemy()
    {
        if (death)
        {
            uI.points += 1000;
            uI.enemiesKilled++;
            Destroy(gameObject);
            death = false;
        }
    }

}
