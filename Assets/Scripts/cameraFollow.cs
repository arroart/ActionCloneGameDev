using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject player;
    public GameObject player1;
    Vector3 newPos;
    float playerYPos;
    float player1YPos;

    public float smoothTime = 0.25f;
    Vector3 velocity;

    float minY;
    public float maxY;
    // Start is called before the first frame update
    void Start()
    {
        minY = transform.position.y;
    }

    private void Update()
    {
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player==null)
        {
            playerYPos = 0f;
        }
        else
        {
            playerYPos = player.transform.position.y;
        }

        if (player1==null)
        {
            player1YPos = 0f;
        }
        else
        {
            player1YPos = player1.transform.position.y;
        }

        if (player==null && player1==null)
        {
            playerYPos = minY;
            player1YPos = minY;
        }

        newPos = new Vector3(transform.position.x, (playerYPos + player1YPos) / 2, transform.position.z);
        if (newPos.y >= minY && newPos.y <= maxY)
        { 
            transform.position = Vector3.SmoothDamp(transform.position,newPos, ref velocity, smoothTime);
        }

        
    }
}
