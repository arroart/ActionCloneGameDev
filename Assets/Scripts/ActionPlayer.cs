using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionPlayer : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;

    public int score = 0;
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

    public int layerNo=6;

    public GameObject otherPlayer;


    public TextMeshProUGUI scoreDisplay;

    private void Start()
    {
        scoreDisplay.text = score.ToString();
        myBody = GetComponent<Rigidbody2D>();
        Invoke("Respawn", 2);
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
                Physics2D.IgnoreLayerCollision(layerNo, 7);
            }
            else
            {
                Physics2D.IgnoreLayerCollision(layerNo, 7,false);
            }
        }
        else
        {
            Physics2D.IgnoreLayerCollision(layerNo, 7);
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
        if (collision.gameObject.tag == "Cloud")
        {
            bounce = true;
            gm.allClouds.Remove(collision.gameObject);
            Destroy(collision.transform.parent.gameObject);
        }
        //if (collision.gameObject == otherPlayer && otherPlayer.GetComponent<Rigidbody2D>().velocity.y < 0 && myBody.velocity.y < 0 && transform.position.y > otherPlayer.transform.position.y)
        //{
        //    score++;
        //    scoreDisplay.text = score.ToString();
        //    otherPlayer.GetComponent<ActionPlayer>().alive = false;
        //    otherPlayer.GetComponent<SpriteRenderer>().flipY = true;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Death" && myBody.velocity.y<0)
        {
            if (alive == true)
            {
                score--;
                scoreDisplay.text = score.ToString();
            }
            Debug.Log("hi");
            alive = false;
            Invoke("Respawn", 2);
        }

    }

    void Respawn()
    {
        alive = true;
        GetComponent<SpriteRenderer>().flipY = false;
        float spawnX = Random.Range(-9f, 9f);
        transform.position = new Vector3(spawnX, spawnY, 0f);
        launch = true;
      
    }

    public void hitPlayer(GameObject collision)
    {
        if (collision.gameObject == otherPlayer && otherPlayer.GetComponent<Rigidbody2D>().velocity.y < 0 && myBody.velocity.y < 0 && transform.position.y > otherPlayer.transform.position.y)
        {
            score++;
            scoreDisplay.text = score.ToString();
            otherPlayer.GetComponent<ActionPlayer>().alive = false;
            otherPlayer.GetComponent<SpriteRenderer>().flipY = true;
        }
    }

}
