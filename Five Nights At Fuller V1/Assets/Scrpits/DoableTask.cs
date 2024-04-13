using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//This class contains all code and logic for the tasks that pop up and you do in the task viewer
public class DoableTask : AbsDoableTask
{
    public Task taskButton;
    public GameObject completeMessage;

    public void Start()
    {
        completeMessage.SetActive(false);
    }

    override
    public void taskComplete()
    {
        completeMessage.SetActive(true);
        finished = true;
        //taskButton.complete = true;
    }
}
