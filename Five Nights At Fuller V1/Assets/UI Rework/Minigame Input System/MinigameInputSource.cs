using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInputSource : MonoBehaviour
{
    [SerializeField] LayerMask RaycastMask = ~0;
    [SerializeField] float RaycastDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        // Shoot a ray from the mouse position on screen
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(mousePosition.origin);

        // Raycast to find what we hit
        RaycastHit hitResult;
        if (Physics.Raycast(mousePosition, out hitResult, RaycastDistance, RaycastMask))
        {
            //ignore if the raycast hasn't hit the target area
            if (hitResult.collider.gameObject != gameObject)
            {
                Debug.Log("Not hit right area");
                return;
            }

            Debug.Log(hitResult.textureCoord);
            //bruh
        }
    }
}
