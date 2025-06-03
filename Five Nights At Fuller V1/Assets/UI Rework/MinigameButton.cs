using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameButton : MonoBehaviour
{

    public Vector2 screenPosition;
    public Vector2 worldPosition; 
    public Camera muhCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
        //screenPosition.z = Camera.main.nearClipPlane + 1;
        worldPosition = muhCamera.ScreenToWorldPoint(screenPosition);

        this.transform.position = worldPosition;
    }

    public void gotPressed()
    {
        Debug.Log("WAHOOOO!");
    }

    public void switchTasks(Camera newCam)
    {
        muhCamera = newCam;
        muhCamera = Camera.main;
    }
}
