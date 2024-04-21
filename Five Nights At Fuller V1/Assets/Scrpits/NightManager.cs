using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//There MUST Be an empty game object in the scene hierarchy with the name "NightManager"
public class NightManager : MonoBehaviour
{
    public bool isHiding;
    public bool lagDelayed;
    public GameObject completeTaskButton;

    public TMP_Text statusText;

    public Task[] nightTasks;
    public Task currentTask;


    // Start is called before the first frame update
    void Start()
    {
        isHiding = false;
        lagDelayed = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTask != null && currentTask.myTask.GetComponent<AbsDoableTask>().finished)
        {
            completeTaskButton.GetComponent<Button>().interactable = true;
            updateStatus(currentTask.taskStatus);
        }
        else
        {
            completeTaskButton.GetComponent<Button>().interactable = false;
            updateStatus("Not Ready");
        }

        
    }

    public void setCurrentTask(Task newTask){
        if (currentTask != null){
            currentTask.myTask.SetActive(false);
        }
        currentTask = newTask;
        currentTask.myTask.SetActive(true);


    }

    public void completeCurrentTask()
    {
        currentTask.completeTask();
        Debug.Log("This should take " + currentTask.completionTime + " Seconds.");
    }

    public void updateStatus(string newText)
    {
        statusText.text = "Status: " + newText;
    }

    /*
    public void completeTask()
    {
        if (currentTask != null)
        {
            float timeToComplete = currentTask.getCompletionTime();

            if (timeToComplete >= 0)
            {
                Debug.Log("This should take " + timeToComplete + " seconds to complete");
                statusText.text = "Working...";
                while (timeToComplete > 0)
                {
                    timeToComplete -= processingTime * Time.deltaTime;
                }

                currentTask.complete = true;
                statusText.text = "Complete";
                Debug.Log("Task Complete!");
            }
        }
    }
    */
}
