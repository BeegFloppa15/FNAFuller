using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public TMP_Text taskNameDisplay;
    public TMP_Text taskDifficultyDisplay;

    public string taskName;
    public int difficulty;
    public bool complete;
    public bool beingCompleted;
    public float completionTime;

    public GameObject myTask;       //MUST BE an AbsDoableTask
    public NightManager myManager;

    // Start is called before the first frame update
    void Start()
    {
        taskNameDisplay.text = taskName;
        taskDifficultyDisplay.text = "Difficulty: " + difficulty;
        myManager = FindFirstObjectByType<NightManager>();
        
        complete = false;
        completionTime = difficulty * Random.Range(0.7f, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (complete)
        {
            this.GetComponent<Button>().interactable = false;
        }

        if (beingCompleted && completionTime > 0)
        {
            completionTime = completionTime - Time.deltaTime;
        }

        if (completionTime <= 0)
        {
            complete = true;
        }
    }

    /*public float getCompletionTime()
    {
        if (myTask.GetComponent<AbsDoableTask>().finished)
        {
            return difficulty * Random.Range(0.7f, 2.0f);
        }
        else
        {
            return -1.0f;
        }
    } */

}
