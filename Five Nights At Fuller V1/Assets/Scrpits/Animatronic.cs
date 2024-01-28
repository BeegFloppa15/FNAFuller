using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatronic : MonoBehaviour
{
    /**Any number from 0-20 */
    public int agression;

    public int locationIndex;
    public GameObject animatronImage;
    [SerializeField] GameObject[] locations;
    [SerializeField] GameObject[] locImages;
    public float moveTimer;
    public float decisionTime;

    // Start is called before the first frame update
    void Start()
    {
        locationIndex = 0;
        moveTimer = 0.0f;
        animatronImage = locImages[0];
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;
        if(moveTimer >= decisionTime)
        {
            DecideToMove();
            moveTimer = 0.0f;
        }

        if (locations[locationIndex].activeInHierarchy)
        {
            animatronImage.SetActive(true);
        }
        else
        {
            animatronImage.SetActive(false);
        }

        //This is to be removed once game is finished. It exists now for dev purposes
        if (Input.GetKeyDown("m")) 
        {
            MoveLocation();
        }

    }

    void DecideToMove()
    {
        if (Random.Range(1, 20) <= agression)
        {
            MoveLocation();
            //Note, the location printed out is NOT what camera they are at, but instead the index
            Debug.Log("Moved to spot " + locationIndex);
        }
        else
        {
            Debug.Log("Staying at spot " + locationIndex);
        }
       
    }


    void MoveLocation()
    {
        if (locationIndex < locations.Length)
        {
            locationIndex++;
        }
        if (locationIndex >= locations.Length)
        {
            locationIndex = 2;

        }
        animatronImage.SetActive(false);
        animatronImage = locImages[locationIndex];
    }
}
