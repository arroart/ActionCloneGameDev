using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollow : MonoBehaviour
{
    public GameObject player;
    public GameObject player1;

    Vector3 playerPos;
    Vector3 player1Pos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<ActionPlayer>().alive)
        {
            playerPos = new Vector3(0f, 0f, 0f);
        }
        else
        {
            playerPos = player.transform.position;
        }
        if (!player1.GetComponent<ActionPlayer>().alive)
        {
            player1Pos = new Vector3(0f, 0f, 0f);
        }
        else
        {
            player1Pos = player1.transform.position;
        }
        Vector3 newPos = new Vector3(0f, (player1Pos.y + playerPos.y) / 2, 0f);
        if(newPos.y>=-5.5f && newPos.y <= 14f)
        {
            transform.position = newPos;
        }
       
    }
}
