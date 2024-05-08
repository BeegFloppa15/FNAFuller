using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtonsV2 : MonoBehaviour
{

    [SerializeField] GameObject currentScreen;     //The screen that is currently active: MUST be set in inspector before game starts
    public NightManager myManager;


    /* --- attemptSwitchCamera ---
     * Called when one of the camera buttons is pressed
     * If player is not lag delayed, we switch to the new screen immediately
     * If player is lag delayed, there is a 2-3 second delay before switching
     * @param newActive: takes in camera's image when the function is called
     */
    public void attemptSwitchCamera(GameObject newActive)
    {
        if (myManager.lagDelayed)
        {
            StartCoroutine(lagDelayCoroutine(newActive));
        }
        else
        {
            switchCamera(newActive);
        }
    }

    /* --- switchCamera ---
     * Changes the currently displayed screen to a new one, passed in when the button is pressed
     */
    public void switchCamera(GameObject newActive)
    {
        currentScreen.SetActive(false);
        newActive.SetActive(true);
        currentScreen = newActive;
        //TO-DO: Add a short blip of static when we switch screens
    }

    /* --- lagDelayCoroutine ---
     * Called if we try to switch cameras while lagDelayed
     * adds a 2-3 second delay before switching cameras
     */
    IEnumerator lagDelayCoroutine(GameObject newActive)
    {
        //TO-DO (Wishlist): Add a scrolling wheel when this is called to indicate we are laging
        yield return new WaitForSeconds(Random.Range(2.0f, 3.2f));
        switchCamera(newActive);
    }
}
