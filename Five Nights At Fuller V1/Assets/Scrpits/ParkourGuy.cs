using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is to be attatched to a spirte object that is the player character for the Parkour doable task
 * The player must have a RigidBody2D component
 * */
public class ParkourGuy : MonoBehaviour
{
    private Rigidbody2D lilGuyRB;   // The RigidBody2D Component of the player object, which we use for movement

    public float guySpeed = 4f;     // A value to change for player's speed
    public float jumpForce = 10f;   // A value to change for player's jump height
    private bool makeItStop;

    public ParkourDoableTask myTask;


    void Start()
    {
        myTask = this.transform.parent.GetComponent<ParkourDoableTask>();
        lilGuyRB = this.GetComponent<Rigidbody2D>();
        makeItStop = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!makeItStop && Input.GetKeyDown("space") && (lilGuyRB.velocity.y >= -1 && lilGuyRB.velocity.y <= 1))
        {
            lilGuyRB.AddForce(new Vector2(0f, 100000f * jumpForce));
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("a") && !makeItStop)
        {
            lilGuyRB.velocity = new Vector2(-50f * guySpeed, lilGuyRB.velocity.y);
            //lilGuyRB.AddForce(new Vector2(-50f * guySpeed, 0f));
        }
        if (Input.GetKey("d") && !makeItStop)
        {
            lilGuyRB.velocity = new Vector2(50f * guySpeed, lilGuyRB.velocity.y);
            //lilGuyRB.AddForce(new Vector2(50f * guySpeed, 0f));
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Exit")
        {
            myTask.taskComplete();
            makeItStop = true;

            //WISH LIST: Add some juice here: confetti or something

        }

        /*
        else if (collision.gameObject.tag == "Wall")
        {
            lilGuyRB.velocity = new Vector2(lilGuyRB.velocity.x, -500f);
        }
        */
    }
}
