using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserBotAnimatronic : Animatronic
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        decisionTime = 7f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    /*
    protected override void MoveLocation()
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
            Debug.Log(this.name + " Moved to:" + locations[locationIndex].name);
        }

    }
    */

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

            }
        }

        locationIndex = 0;
        animatronImage.SetActive(false);
        animatronImage = locImages[locationIndex];
        movementEnabled = true;
        Debug.Log("Couldn't JumpScare, moving back to IMGD HALL");
    }
}