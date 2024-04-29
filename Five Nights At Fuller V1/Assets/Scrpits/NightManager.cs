using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

//There MUST Be an empty game object in the scene hierarchy with the name "NightManager"
public class NightManager : MonoBehaviour
{
    public bool isHiding;
    public bool lagDelayed;
    public GameObject completeTaskButton;

    public TMP_Text statusText;

    public Task[] nightTasks;
    public Task currentTask;

    public GameObject leaveCanvas;
    public GameObject winScreen;

    public EventSystem eve;

    // Start is called before the first frame update
    void Start()
    {
        isHiding = false;
        lagDelayed = false;
        eve = FindFirstObjectByType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTask != null && currentTask.myTask.GetComponent<AbsDoableTask>().finished)   //We can only press the button to finish a task
        {                                                                                       //if it's DoableTask has been finished
            completeTaskButton.GetComponent<Button>().interactable = true;
            updateStatus(currentTask.taskStatus);
        }
        else
        {
            completeTaskButton.GetComponent<Button>().interactable = false;
            updateStatus("Not Ready");
        }

        
    }

    /* This function is called when a Task button in the TaskList scroll view is clicked
     * It disables the current doable task (if there is one) and enables the newly selected task
     * It also changes which Task object is stored as the current task
     */
    public void setCurrentTask(Task newTask){       
        if (currentTask != null){
            currentTask.myTask.SetActive(false);
        }
        currentTask = newTask;
        currentTask.myTask.SetActive(true);

        eve.SetSelectedGameObject(null);
    }

    /* This is called when the "Complete Task" Button is called
     * It calls the completion function of the stored current task
     */
    public void completeCurrentTask()
    {
        currentTask.completeTask();
        Debug.Log("This should take " + currentTask.completionTime + " Seconds.");
    }

    /* This changes the status text displayed in the task view
     */
    public void updateStatus(string newText)
    {
        statusText.text = "Status: " + newText;
    }

    public void updateStatusforCams()
    {
        foreach (Task thisTask in nightTasks)
        {
            thisTask.taskStatus = "Ready to Complete Task!";
        }
    }

    /* This is called when a task has been completed
     * It checks if all tasks have been completed, and if they have, it displays the option to leave. 
     */
    public void checkAllTasksDone()
    {
        foreach (Task thisTask in nightTasks)
        {
            if (!thisTask.complete)
            {
                return;
            }
        }

        //If we reach here, all tasks have been finished
        Debug.Log("All Tasks Finished!");
        leaveCanvas.SetActive(true);
    }

    /* This is called when the "Log off" button on the leave screen is pressed
     * It activates the win screen and makes it fade in with the Fade and load coroutine
     * 
     * !!! TO-DO !!! - Make this de-activate the animatronics when we win
     */
    public void winNight()
    {
        Debug.Log("Congrats! You Win!");
        winScreen.SetActive(true);
        StartCoroutine(fadeAndLoadCoroutine());

    }

    /* This is called by the winNight() method
     * Makes the winScreen Fade in rather than just instantly popping in
     * 
     * !!! TO-DO !!! - Make this load the next scene after waiting a few sconds
     */
    IEnumerator fadeAndLoadCoroutine()
    {
        CanvasGroup myGroup = winScreen.GetComponent<CanvasGroup>();

        while (myGroup.alpha < 1)
        {
            myGroup.alpha += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(5.5f);
        Debug.Log("Load Next scene");
    }

    
}
