using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GompeiBotAnimatronic : Animatronic
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        decisionTime = 2.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (locationIndex == 6 && locations[6].activeInHierarchy)
        {
            StartCoroutine(gompeiFleeCoroutine());
        }
    }

    IEnumerator gompeiFleeCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));

        if (locationIndex == 6 && locations[6].activeInHierarchy)
        {
            moveTimer = 0f;
            if (Random.Range(1f, 4f) <= 3f)
            {
                //Move forward into office
                MoveLocation();
            }
            else
            {
                //Move Back into Fuller Upper
                locationIndex--;
                animatronImage.SetActive(false);
                animatronImage = locImages[locationIndex];
                Debug.Log("Moved Back to Flupper");
            }
        }
    }

    protected override IEnumerator attemptJumpscareCoroutine()
    {
        Debug.Log("Gompei's gonna getcha!");

        //yield return new WaitForSeconds(Random.Range(5f, 10f));
        yield return new WaitForSeconds(2f);
        if (!myManager.isHiding)
        {
            Debug.Log("BOO! Jumpscare");
        }
        else
        {
            //locationIndex = Random.Range(1, 3);
            locationIndex = 6;
            animatronImage.SetActive(false);
            animatronImage = locImages[locationIndex];
            movementEnabled = true;
            Debug.Log("Failed to spook, went back to " + locations[locationIndex].name);
        }
    }
}
