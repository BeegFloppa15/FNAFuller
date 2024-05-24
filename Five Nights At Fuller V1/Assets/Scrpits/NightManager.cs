using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//There MUST Be an empty game object in the scene hierarchy with the name "NightManager"
public class NightManager : MonoBehaviour
{
    //PLAYER STATUS
    public bool isHiding;                   // True if player is hiding under desk, false otherwise
    public bool camsOpen;                   // True if cameras/camera view is open, false otherwise
    public bool lagDelayed;                 // True if Starship Robot is in Server Room, false otherwise
    
    //VISIBLE TASK VIEW ELEMENTS
    public GameObject completeTaskButton;   // A button to activate a task's completion timer
    public TMP_Text statusText;             // Text displayed below task view
    public GameObject leaveCanvas;          // To be displayed when all tasks are finished, allowing player to finish the night and win
    public GameObject winScreen;            // To be displayed when player presses the button to leave, finishing the night and winning

    //INVISIBLE MANAGEMENT THINGS
    public Task[] nightTasks;               // A list of all the tasks that need to be done in this night
    public Task currentTask;                // The current task that is selected and activated
    public EventSystem eve;                 // The event system. We reference it for like... one thing. It's stupid
    public GameObject myCamera;             // The Main Camera that is used for the 2D Game

    //ANIMATRONICS
    [SerializeField] Animatronic gompeiBot;
    [SerializeField] Animatronic hackerBot;
    [SerializeField] Animatronic taserBot;

    //ANIMATRONIC AGGRESSION MANAGEMENT
    public int numAggressionIncreases;      // Number of times the animatronic's agression will increase during the night, MUST BE > 0
    public int aggressionIncrease;          // Increases animatronic agression by this number
    private int tasksBeforeIncrease;
    private int tasksCompleted;

    // Start is called before the first frame update
    void Start()
    {
        isHiding = false;
        lagDelayed = false;
        camsOpen = false;
        eve = FindFirstObjectByType<EventSystem>();
        if (numAggressionIncreases != 0)
        {
            tasksBeforeIncrease = nightTasks.Length / (numAggressionIncreases + 1);
        }
        else
        {
            Debug.Log("ERROR: numAggressionIncreases must be more than 1 to prevent an error");
        }

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

    /* Changes the hiding status, to be called when Hiding Buttons are pressed
     * Takes in a bool value
     * Uses the next two coroutines below
     */
    public void setHidingStatus(bool bruh)
    {
        isHiding = bruh;

        if (bruh)
        {
            StartCoroutine(hideCamCoroutine());
        }
        else
        {
            StartCoroutine(stopHideCamCoroutine());
        }
    }

    IEnumerator hideCamCoroutine()
    {
        float movementSpeed = 1500.0f;
        while (myCamera.transform.position.y > -1200)
        {
            myCamera.transform.Translate(new Vector2(0f, -1f) * movementSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator stopHideCamCoroutine()
    {
        float movementSpeed = 1000.0f;
        while (myCamera.transform.position.y < 0)
        {
            myCamera.transform.Translate(new Vector2(0f, 1f) * movementSpeed * Time.deltaTime);
            yield return null;
        }
        myCamera.transform.position = new Vector3(0f, 0f, -10f);
    }


    /* Changes the camsOpen bool, to be called when Open Cams and Close Cams buttons are pressed
     * Takes in a bool value
     */
    public void setCamStatus(bool bruh)
    {
        camsOpen = bruh;
    }

    /* --- setCurrentTask ---
     * This function is called when a Task button in the TaskList scroll view is clicked
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


    /* --- completeCurrentTask ---
     * This is called when the "Complete Task" Button is called
     * It calls the completion function of the stored current task
     */
    public void completeCurrentTask()
    {
        currentTask.completeTask();
        Debug.Log("This should take " + currentTask.completionTime + " Seconds.");
    }

    /* --- updateStatus ---
     * This changes the status text displayed in the task view
     */
    public void updateStatus(string newText)
    {
        statusText.text = "Status: " + newText;
    }



    /* --- checkAllTasksDone ---
     * This is called when a task has been completed
     * It checks if all tasks have been completed, and if they have, it displays the option to leave. 
     * ALSO: If a certain percentage of tasks are completed, we will increase each animatronic's aggression
     */
    public void checkAllTasksDone()
    {
        //Checking to see if enough tasks have been completed to increase animatronic aggression
        tasksCompleted++;
        if (tasksCompleted >= tasksBeforeIncrease && numAggressionIncreases > 0)
        {
            //Increase all animatronic aggression
            gompeiBot.increaseAggression(aggressionIncrease);
            hackerBot.increaseAggression(aggressionIncrease + 1);
            taserBot.increaseAggression(aggressionIncrease);
            numAggressionIncreases--;
        }

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

    /* --- winNight ---
     * This is called when the "Log off" button on the leave screen is pressed
     * It activates the win screen and makes it fade in with the Fade and load coroutine
     * 
     * !!! TO-DO !!! - Make this de-activate the animatronics when we win
     */
    public void winNight()
    {
        //Disable all Animatronics
        gompeiBot.gameObject.SetActive(false);
        hackerBot.gameObject.SetActive(false);

        //Activate Win screen
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


    /* --- jumpscareAndLose(float) ---
     * Called when an animatronic jumpscares the player
     * Uses the coroutine below to wait a short while, then load the game over screen
     * Takes in a float value to determine how long to wait (in seconds)
     * */
    public void jumpscareAndLose(float t)
    {
        StartCoroutine(jumpscareCoroutine(t));
    }

    IEnumerator jumpscareCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene("GameOverScene");

    }

    
}
