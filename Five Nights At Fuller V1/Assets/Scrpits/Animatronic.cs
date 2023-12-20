using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatronic : MonoBehaviour
{
    //Any number from 0-20
    public int agression;
    public int locationIndex;
    public GameObject animatronImage;
    [SerializeField] GameObject[] locations;

    // Start is called before the first frame update
    void Start()
    {
        locationIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (locations[locationIndex].activeInHierarchy)
        {
            animatronImage.SetActive(true);
        }
        else
        {
            animatronImage.SetActive(false);
        }

        if (Input.GetKeyDown("m")) 
        {
            MoveLocation();
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
            locationIndex = 2;
        }
    }
}
