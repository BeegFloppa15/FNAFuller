using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourDoableTask : AbsDoableTask
{

    public GameObject littleGuy;

    public GameObject exit;

    override
    public void taskComplete()
    {
        finished = true;

    }


}
