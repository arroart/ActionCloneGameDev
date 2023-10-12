using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;

    public float xAccel;
    public float gravity;
    public float bounceVel;

    bool goLeft;
    bool goRight;
    Rigidbody2D myBody;

    public GameManager gm;

    public float launchVel = 20f;
    public bool launch = false;
    public int playerNum;

    bool bounce = false;

    public float spawnY = -13f;

    public bool alive = true;

    private void Start()
    { 
        myBody = GetComponent<Rigidbody2D>();
        Respawn();
    }

    private void Update()
    {
        if (alive)
        {
            if (Input.GetKey(leftKey))
            {
                goLeft = true;
            }
            else
            {
                goLeft = false;
            }

            if (Input.GetKey(rightKey))
            {
                goRight = true;
            }
            else
            {
                goRight = false;
            }

            if (myBody.velocity.y > 0)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
            else
            {
                gameObject.GetComponent<Collider2D>().isTrigger = false;
            }
        }
        else
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 newVel = myBody.velocity;

        newVel.x *= 0.9f;

        newVel.y += gravity;

        if (bounce)
        {
            newVel.y += bounceVel;
            bounce = false;
        }

        if( goLeft)
        {
            newVel.x -= xAccel;
        }
        else if (goRight)
        {
            newVel.x += xAccel;
        }

        if (launch){
            newVel.y += launchVel;
            launch = false;
        }

        myBody.velocity = newVel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Cloud")
        {
            bounce = true;
            Destroy(collision.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Death" && myBody.velocity.y<0)
        {
            Invoke("Respawn", 5);
        }
    }

    void Respawn()
    {
        alive = true;
        float spawnX = Random.Range(-9f, 9f);
        transform.position = new Vector3(spawnX, spawnY, 0f);
        launch = true;
      
    }
}
