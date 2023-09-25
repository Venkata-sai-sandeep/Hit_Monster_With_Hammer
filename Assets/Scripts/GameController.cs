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
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private int presentMonsterIndex;
    [SerializeField] private int Playerscore = 0;
    [SerializeField] private int playerLives = 5;
    private string timer, time;
    private float currentTime, countdownDuration;
    [SerializeField]
    private bool isTimeOut = false;
    [SerializeField] private bool isGameOver = false;
    private float waitTime = 5f;
    private List<int> playerIndexes = new List<int>();
    void Start()
    {
        timer = TimerSelection.text;
        Playerscore = 0;
        scoreDisplayer.text ="Score : " +Playerscore.ToString();
        if (TimerSelection.text != "Select Time")
        {
            //if(timer == "")
            DropDownObject.SetActive(false);
            char[] ch = timer.ToCharArray();
            countdownDuration = int.Parse(ch[0].ToString());//(float)timer;
            StartCountdown();
            startGame();
            //startProcess(3f);
        }

    }

    // Update is called once per frame
    void Update()
    {
       if(isTimeOut)
       {
            InstructionDisplayer.text = "Time Up! Your Score : " + Playerscore.ToString();
       }
       if(playerLives <0)
       {
            InstructionDisplayer.text = "Game Over! Your Score : " + Playerscore.ToString();
       }
       if(Playerscore < 0)
        {
            InstructionDisplayer.text = "Game Over! Your Score : " + playerIndexes.ToString();
            isGameOver = true;
        }
    }
    private void startGame()
    {
        if(!isTimeOut && playerLives >=0 && !isGameOver)
        {
            int randomValue = Random.Range(0, monsters.Length);
            presentMonsterIndex = randomValue;
            monsters[presentMonsterIndex].SetActive(true);
            playerIndexes.Add(presentMonsterIndex);
            StartCoroutine(playerNotHitOnTime(waitTime, playerIndexes.Count));
            
            //playerIndexes[playerIndexes.Length] = presentMonsterIndex;
        }
        
    }
   

    public void onHitMonster()
    {
        if(!isTimeOut && playerLives >=0 && !isGameOver)
        {
           
            scoreDisplayer.text ="Score : " +Playerscore.ToString();
            monsters[presentMonsterIndex].SetActive(false);
            int randomValue = Random.RandomRange(0, monsters.Length);
            presentMonsterIndex = randomValue;
            monsters[presentMonsterIndex].SetActive(true);
            playerIndexes.Add(presentMonsterIndex);
            if (Playerscore > 100)
                waitTime = 3f;
            if (Playerscore > 250)
                waitTime = 2f;
            if (Playerscore > 400)
                waitTime = 1f;
            if (Playerscore > 500)
                waitTime = 0.5f;
            StartCoroutine(playerNotHitOnTime(waitTime, playerIndexes.Count));
            //Debug.Log("PLAYER : " + playerIndexes[playerIndexes.Count - 1]);
        }
       
    }

    public void AddPlayerScore(int x)
    {
        Playerscore += x;
    }
    IEnumerator playerNotHitOnTime(float waitTime, int playerIndex)
    {
        if(playerLives >= 0)
        {
            yield return new WaitForSeconds(waitTime);
            if (playerIndex == playerIndexes.Count)
            {
                //Debug.Log("Player index : "+ playerI)
                //Debug.Log("Player Indexes count : "+playerIndexes.Count);
                monsters[presentMonsterIndex].SetActive(false);
                playerLives--;
                hearts[playerLives].SetActive(false);
                Playerscore -= 10;
                scoreDisplayer.text = "Score : " + Playerscore.ToString();
                onHitMonster();
            }


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
        if(!isGameOver && !isTimeOut)
        {
            while (currentTime > 0)
            {
                yield return new WaitForSeconds(1f); // Wait for 1 second
                currentTime--;
                if (currentTime == 0) isTimeOut = true;
                UpdateTimerDisplay();
            }
        }
        

        // The countdown has reached 0 seconds
        // Perform any actions you want to execute when the countdown is complete
    }

    private void UpdateTimerDisplay()
    {
        // Update your UI or console display with the current time
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        string secondsDup;
        if(seconds < 10)
        {
            secondsDup = "0" + seconds.ToString();
        }
        else
        {
            secondsDup = seconds.ToString();
        }
        InstructionDisplayer.text = minutes + ":" + secondsDup;
        if(minutes == 0 && seconds < 30)
        {
            InstructionDisplayer.gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        //Debug.LogFormat("{0}:{1:00}", minutes, seconds);
    }

}
