using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject otherHit;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == otherHit)
        {
            player.GetComponent<ActionPlayer>().hitPlayer(otherHit.GetComponent<HitBox>().player);
        }
        
    }

}
