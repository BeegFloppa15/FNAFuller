using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Task : MonoBehaviour
{
    public TMP_Text taskNameDisplay;
    public TMP_Text taskDifficultyDisplay;

    public string taskName;
    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        taskNameDisplay.text = taskName;
        taskDifficultyDisplay.text = "Difficulty: " + difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
