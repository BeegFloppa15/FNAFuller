using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public TMP_Text taskNameDisplay;
    public TMP_Text taskDifficultyDisplay;

    public string taskName;         // Will Change Task Name on the button object
    public int difficulty;          // Determines Task Difficulty, and how long it will take to complete
    public bool complete;           // The WHOLE TASK is done and completed
    public float completionTime;    // How long it would normally take to finish the task (not lag delayed)
    public string taskStatus;       // TO be displayed on task screen by NightManager

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
        taskStatus = "Ready to Complete Task!";
    }

    // Update is called once per frame
    void Update()
    {
        if (complete)
        {
            this.GetComponent<Button>().interactable = false;

        }
        
    }

    
    public void completeTask()
    {
        Coroutine myCoroutine = StartCoroutine(taskTimerCoroutine());
    }


    IEnumerator taskTimerCoroutine()
    {
        while (completionTime > 0)
        {
            if (!myManager.lagDelayed)
            {
                completionTime = completionTime - Time.deltaTime;
                taskStatus = "Working...";
            }
            else
            {
                completionTime = completionTime - (Time.deltaTime * 0.01f);
                taskStatus = "buffering...";
            }
            yield return null;
        }

        Debug.Log("Task Complete!");
        complete = true;
        taskStatus = "Complete!";
        myManager.checkAllTasksDone();

    }

}
