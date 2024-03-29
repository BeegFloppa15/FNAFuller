using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatronic : MonoBehaviour
{
    /**Any number from 0-20 */
    public int agression;

    public int locationIndex;                   //An index of where the animatronic is in the building
    public GameObject animatronImage;           //The image that is displayed of the animatronic on cameras
    [SerializeField] GameObject[] locations;    //An array of the camera locations, in the order that THIS SPECIFIC ANIMATRONIC goes on
    [SerializeField] GameObject[] locImages;    //A list of images which are different based on the animatronics location. Go in the same order as the locations
    public float moveTimer;                     //A float that increases with time
    public float decisionTime;                  //When the move timer is greater than this number, we make a decision to move.
    public GameObject hidingScreen;

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
            attemptJumpscare();
            //locationIndex = 2;

        }
        animatronImage.SetActive(false);
        animatronImage = locImages[locationIndex];
    }

    void attemptJumpscare()
    {
        float scareTimer = 0.0f;
        float decisionTime = Random.Range(5, 10);

        Debug.Log("Will attempt to jumpscare in " + decisionTime + "seconds");

        while (scareTimer < decisionTime)
        {
            scareTimer += Time.deltaTime;
        }

        if (hidingScreen.activeInHierarchy)
        {
            Debug.Log("BOO! Jumpscare");
        }
        else
        {
            locationIndex = 2;
            Debug.Log("Failed to spook, went back to spot 2");
        }
        
    }
}
