using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerBotAnimatronic : Animatronic
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        decisionTime = 6f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override IEnumerator attemptJumpscareCoroutine()
    {
        Debug.Log("Hacker bot will start hacking the system soon");

        yield return new WaitForSeconds(Random.Range(5.5f, 11f));

        if (locationIndex == locations.Length - 1)
        {
            Debug.Log("Hacking the servers!");
            myManager.lagDelayed = true;
        }
    }

    public void getZapped()
    {
        if (locationIndex == locations.Length - 1)
        {
            StartCoroutine(zapCoroutine());
        }
    }

    protected IEnumerator zapCoroutine()
    {
        Debug.Log("YOUCH! Hacker Bot is leaving");

        yield return new WaitForSeconds(2f);
        myManager.lagDelayed = false;
        locationIndex = 1;
        animatronImage.SetActive(false);
        animatronImage = locImages[locationIndex];
        movementEnabled = true;
    }
}
