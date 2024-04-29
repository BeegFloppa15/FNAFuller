using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourGuy : MonoBehaviour
{

    public ParkourDoableTask myTask;
    // Start is called before the first frame update
    void Start()
    {
        myTask = this.transform.parent.GetComponent<ParkourDoableTask>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Exit")
        {
            myTask.taskComplete();
            Debug.Log("Parkour Task Done");
        }
    }
}
