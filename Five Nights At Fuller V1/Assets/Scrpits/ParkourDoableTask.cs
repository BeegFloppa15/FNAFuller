using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourDoableTask : AbsDoableTask
{

    public GameObject littleGuy;
    private Rigidbody2D lilGuyRB;
    public GameObject exit;

    public float guySpeed = 4f;
    public float jumpForce = 10f;

    private bool makeItStop;


    // Start is called before the first frame update
    void Start()
    {
        lilGuyRB = littleGuy.GetComponent<Rigidbody2D>();
        makeItStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!makeItStop && Input.GetKeyDown("space") && (lilGuyRB.velocity.y >= -1 && lilGuyRB.velocity.y <= 1))
        {
            lilGuyRB.AddForce(new Vector2(0f, 100000f * jumpForce));
            Debug.Log("Jump!");
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("a") && !makeItStop)
        {
            lilGuyRB.velocity = new Vector2(-50f * guySpeed, lilGuyRB.velocity.y);
        }
        if (Input.GetKey("d") && !makeItStop)
        {
            lilGuyRB.velocity = new Vector2(50f * guySpeed, lilGuyRB.velocity.y);
        }

        
    }

    override
    public void taskComplete()
    {
        finished = true;
        makeItStop = true; 
    }


}
