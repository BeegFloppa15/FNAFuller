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

    /* Should be called by NightManager to start counting down this task's timer
     */
    public void completeTask()
    {
        Coroutine myCoroutine = StartCoroutine(taskTimerCoroutine());
    }

    /* Keeps track of the timer counting down for when the task should be completed
     * NOTE: There is a delay penalty (decreasing the ammount of time taken off the timer) if the following states are true:
     * - The player is watching the cams (light penalty)
     * - The player is hiding (light penalty)
     * - Starship bot is hacking the servers (HEAVY PENALTY)
     * These states should be controlled by the Night Manager
     * These penalties SHOULD NOT STACK
     */
    IEnumerator taskTimerCoroutine()
    {
        while (completionTime > 0)
        {
            if (myManager.lagDelayed)
            {
                completionTime = completionTime - (Time.deltaTime * 0.01f);
                taskStatus = "buffering...";
            }
            else if (myManager.isHiding || myManager.camsOpen)
            {
                completionTime = completionTime - (Time.deltaTime * 0.2f);
                taskStatus = "Working...";
            }
            else
            {
                completionTime = completionTime - Time.deltaTime;
                taskStatus = "Working...";
            }
            
            yield return null;
        }

        Debug.Log("Task Complete!");
        complete = true;
        taskStatus = "Complete!";
        myManager.checkAllTasksDone();

    }

}
