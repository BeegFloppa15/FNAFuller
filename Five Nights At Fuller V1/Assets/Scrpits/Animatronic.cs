using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animatronic : MonoBehaviour
{
    //Any number from 0-20
    public int agression;
    public int locationIndex;
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
            this.GetComponent<Image>().SetActive(true);
        }
        else
        {
            this.GetComponent<Image>().SetActive(false);
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
