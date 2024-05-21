using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserBotAnimatronic : Animatronic
{
    public Animator animator;                   //Animator that is attatched to bot, used for the jumpscare
    public SpriteRenderer spriteRenderer;       //Sprite Renderer that is attatched to bot, used for the jumpscare (disabled until jumpscare starts)
 
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        decisionTime = 7f;
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    // THIS CAUSED A CRASH THE LAST TIME WE RAN IT
    protected override IEnumerator attemptJumpscareCoroutine()
    {
        yield return null;
        while (myManager.lagDelayed)
        {
            yield return null;
            if (myManager.isHiding)
            {
                yield return new WaitForSeconds(1.5f);
                Debug.Log("YOU GOT TASED!!!");
                spriteRenderer.enabled = true;
                animator.SetTrigger("Activate Jumpscare");
                myManager.jumpscareAndLose(0.7f);

            }
        }

        locationIndex = 0;
        animatronImage.SetActive(false);
        animatronImage = locImages[locationIndex];
        movementEnabled = true;
        Debug.Log("Couldn't JumpScare, moving back to IMGD HALL");
    }
}