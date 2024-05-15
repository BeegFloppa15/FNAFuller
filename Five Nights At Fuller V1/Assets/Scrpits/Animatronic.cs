using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatronic : MonoBehaviour
{
    
    public int agression;                       // Number from 0-20, determines how likely it is that an animatronic will move
    public int locationIndex;                   //An index of where the animatronic is in the building
    public GameObject animatronImage;           //The image that is displayed of the animatronic on cameras
    [SerializeField] protected GameObject[] locations;    //An array of the camera locations, in the order that THIS SPECIFIC ANIMATRONIC goes on
    [SerializeField] protected GameObject[] locImages;    //A list of images which are different based on the animatronics location. Go in the same order as the locations
    public float moveTimer;                     //A float that increases with time
    public float decisionTime;                  //When the move timer is greater than this number, we make a decision to move.
    public NightManager myManager;
    public bool movementEnabled;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        locationIndex = 0;
        moveTimer = 0.0f;
        animatronImage = locImages[0];
        myManager = FindFirstObjectByType<NightManager>();
        movementEnabled = true;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Counting the Movement timer
        if (movementEnabled)
        {
            moveTimer += Time.deltaTime;
        }

        // Deciding to move, then reset move timer
        if(moveTimer >= decisionTime)
        {
            DecideToMove();
            moveTimer = 0.0f;
        }

        // If we open the camera location that the animatronic is on, the animatronic's image for that location is activated
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

    /** --- DecideToMove() ---
     *  This is called once the move timer exceeds the decision time.
     *  The animatronic will either call the MoveLocation function to move to the next spot, or it will stay in the same spot
     *  If agression is lower, moving will be less likely. If it is higher, it will be more likely
     */
    protected void DecideToMove()
    {
        if (Random.Range(1, 20) <= agression)
        {
            MoveLocation();
        }
        else
        {
            Debug.Log("Staying at " + locations[locationIndex].name);
        }
       
    }

    /** --- MoveLocation()---
     * This is called by the DecideToMove() function
     * The animatronic moves to the next spot in its locations list. This is done by
     *  1) Incrementing the location index 
     *  2) Setting the image used for the animatronic to the next one in the images list
     * 
     * If the animatronic reaches the last spot in its list (usually the office where it would pose the most danger),
     * The animatronic will stop moving, and attempt to jumpscare using the attempJumpscareCoruoutine
     */
    protected void MoveLocation()
    {
        
         locationIndex++;
        
        if (locationIndex >= locations.Length - 1)
        {
            movementEnabled = false;
            StartCoroutine(attemptJumpscareCoroutine());
        }
        else
        {
            animatronImage.SetActive(false);
            animatronImage = locImages[locationIndex];
            Debug.Log("Moved to:" + locations[locationIndex].name);
        }

    }


    /** --- attemptJumpscareCoroutine() ---
     * This coroutine is called when the animatronic moves into their final position, like the office for example
     * The animatronic will wait for a brief grace period, and then attempt to jumpscare the player.
     * In this case, if the player is not hiding, we will jumpscare the player
     */
    protected virtual IEnumerator attemptJumpscareCoroutine()
    {
        Debug.Log("Will attempt to jumpscare shortly");

        yield return new WaitForSeconds(Random.Range(5f, 10f));

        if (!myManager.isHiding)
        {
            Debug.Log("BOO! Jumpscare");
        }
        else
        {
            locationIndex = 2;
            Debug.Log("Failed to spook, went back to spot 2");
            animatronImage.SetActive(false);
            animatronImage = locImages[locationIndex];
            movementEnabled = true;
        }
    }

    /** --- increaseAggression(int)
     * This method increases the animatronic's aggression
     * Takes in an int, and increase aggression BY THAT AMMOUNT
     */
    public void increaseAggression(int i)
    {
        agression += i;
    }
}
