using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GompeiBotAnimatronic : Animatronic
{
    public Animator animator;                   //Animator that is attatched to Gompei, used for the jumpscare
    public SpriteRenderer spriteRenderer;       //Sprite Renderer that is attatched to Gompei, used for the jumpscare (disabled until jumpscare starts)

    public Animator upperLobbyAnimator;         //Animator that is used for the upper lobby location 

    public AudioSource leaveSound;              //Sound that plays if and when Gompei leaves your office
    public AudioSource jumpscareSound;          //Sound that plays if and when Gompei Jumpscares you

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        spriteRenderer.enabled = false;
        decisionTime = 5.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //If we are looking at Gompei through the Upper lobby cam, he might jump forward into the office or go back to Fuller Upper
        if (locationIndex == 6 && locations[6].activeInHierarchy)
        {
            StartCoroutine(gompeiFleeCoroutine());
        }
    }

    IEnumerator gompeiFleeCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));

        //If we are still looking through Upper Lobby cam at gompei, then he will move
        if (locationIndex == 6 && locations[6].activeInHierarchy)
        {
            moveTimer = 0f;
            if (Random.Range(1f, 4f) <= 3f)
            {
                //Move forward into office
                upperLobbyAnimator.SetTrigger("AdvanceToOffice");;
                MoveLocation();
            }
            else
            {
                //Move Back into Fuller Upper
                locationIndex--;
                upperLobbyAnimator.SetTrigger("RetreatToFlupper");
                animatronImage.SetActive(false);
                animatronImage = locImages[locationIndex];
                Debug.Log("Moved Back to Flupper");
            }
        }
    }

    protected override IEnumerator attemptJumpscareCoroutine()
    {
        Debug.Log("Gompei's gonna getcha!");

        yield return new WaitForSeconds(Random.Range(5f, 10f));

        //If we aren't hiding, Gompei will jumpscare us
        if (!myManager.isHiding)
        {
            jumpscareSound.Play();
            Debug.Log("BOO! Jumpscare");
            spriteRenderer.enabled = true;
            animator.SetTrigger("Begin Jumpscare");
            myManager.jumpscareAndLose(0.56f);
        }

        //If we are hiding, he will go back to either the basement hallway, the lower lobby, or the IMGD Hallway
        else
        {
            leaveSound.Play();

            locationIndex = Random.Range(1, 3);
            animatronImage.SetActive(false);
            animatronImage = locImages[locationIndex];
            movementEnabled = true;
            Debug.Log("Failed to spook, went back to " + locations[locationIndex].name);
        }
    }

    
}
