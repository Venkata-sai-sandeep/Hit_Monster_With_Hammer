using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI TimerSelection, scoreDisplayer,InstructionDisplayer;
    [SerializeField]
    private GameObject DropDownObject;
    [SerializeField]
    private GameObject[] monsters;

    private string timer, time;
    private float currentTime, countdownDuration;
    [SerializeField]
    private bool isTimeOut = false;
    [SerializeField]
    private int score;
    void Start()
    {
        timer = TimerSelection.text;
        if (TimerSelection.text != "Select Time")
        {
            //if(timer == "")
            DropDownObject.SetActive(false);
            char[] ch = timer.ToCharArray();
            countdownDuration = int.Parse(ch[0].ToString());//(float)timer;
            StartCountdown();
            //startProcess(3f);
        }

    }

    // Update is called once per frame
    void Update()
    {
       if(!isTimeOut)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void Process()
    {
        if(!isTimeOut)
        {

        }
    }

    IEnumerator timerChecker(float x)
    {
        yield return new WaitForSeconds(x);
        if (!InstructionDisplayer.gameObject.activeSelf)
        {
            InstructionDisplayer.gameObject.SetActive(true);
        }
        else if (InstructionDisplayer.gameObject.activeSelf)
        {
            InstructionDisplayer.gameObject.SetActive(false);
        }
            
    }

    private void StartCountdown()
    {
        currentTime = countdownDuration * 60;
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            currentTime--;
            if (currentTime == 0) isTimeOut = true;
            UpdateTimerDisplay();
        }

        // The countdown has reached 0 seconds
        // Perform any actions you want to execute when the countdown is complete
    }

    private void UpdateTimerDisplay()
    {
        // Update your UI or console display with the current time
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        InstructionDisplayer.text = minutes + ":" + seconds;
        if(minutes == 0 && seconds < 30)
        {
            InstructionDisplayer.gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        //Debug.LogFormat("{0}:{1:00}", minutes, seconds);
    }

}
