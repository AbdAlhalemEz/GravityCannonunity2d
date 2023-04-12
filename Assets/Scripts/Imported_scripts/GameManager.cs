using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;


//demo version trying to push
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance
    //public TextMeshProUGUI scoreText; // Text for score
    private int score = 0; // Score
    private int trigger;
    public int scoreNeeded = 3;
    private bool insideZone = false;
    private int numTries = 0;
    private int[] massArray = { 5, 10, 1 };
    private int massBall = 5;
    public int level = 1; // Score

    //Get the student ID
    public int id;
    public static int gameid = 1;
    private float startTime;
    public int distBlocks = 0;
    public float leveltime;
    public int difficultyFactor;
    public int onoff_value = 1;

    private void Start()
    {

        // Keep this object even when we go to new scene
        if (instance == null)
        {
            instance = this;
        }

        id = PlayerPrefs.GetInt("MyId");
        Debug.Log("hhhhhhhhhhhhhhh" + id);
        //Debug.Log(SceneManager.GetActiveScene().buildIndex+1);

        //instructor change game on off
        StartCoroutine(FetchData());

    }

    // Update is called once per frame
    void Update()
    {


        winState();
    }

    //get the data from control table
    IEnumerator FetchData()
    {
        // Replace "your_php_script.php" with the filename of your PHP script
        UnityWebRequest www = UnityWebRequest.Get("https://advgamin.000webhostapp.com/control.php");

        // Disable SSL verification
        www.certificateHandler = new AcceptAllCertificates();

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error fetching data from database: " + www.error);
        }
        else
        {
            onoff_value = int.Parse(www.downloadHandler.text);
            Debug.Log("Value of onoff column: " + onoff_value);
        }

        if (onoff_value == 0)
        {
            Debug.LogError("Variable v is not set correctly. Quitting the application.");
            SceneManager.LoadScene("Login_page");
        }



    }




    //leave the game
    private void OnApplicationQuit()
    {


        SaveGameTime(id, gameid, startTime);


    }




    public void addScore(int id, int gameId, int score, int level)
    {
        // Create a new ScoreManager object in the scene
        GameObject scoreManagerObj = new GameObject("ScoreManager");
        ScoreManager scoreManager = scoreManagerObj.AddComponent<ScoreManager>();

        // Set the player name and score
        scoreManager.playerId = id;
        scoreManager.gameId = gameid;
        scoreManager.score = score;
        scoreManager.level = level;

        // Add the score to the database
        scoreManager.AddScoreToDatabase();
    }


    public void SaveGameTime(int playerId, int gameId, float startTime)
    {

        float totalTime = Time.time - startTime;
        int totalSeconds = Mathf.RoundToInt(totalTime);
        Debug.Log("Total time played: " + totalSeconds + " seconds");




        // Create a new timeManager object in the scene
        GameObject timeManagerObj = new GameObject("timeManager");
        timeManager timeManager = timeManagerObj.AddComponent<timeManager>();

        // Set the player ID, game ID, time spent, and last played fields
        timeManager.playerId = playerId;
        timeManager.gameId = gameId;
        timeManager.time_spent = totalSeconds;

        // Call the AddTimeToDatabase method on the timeManager object
        timeManager.AddTimeToDatabase();
    }


    // Update the score on the hud
    public void ChangeScore()
    {
        distBlocks++;

    }



    public void setMass(int i)
    {
        massBall = massArray[i];
    }

    public int getMass()
    {
        return massBall;
    }

    public int getTries()
    {
        return numTries;
    }

    public int getAD()
    {
        return distBlocks;
    }

    public int getSN()
    {
        return scoreNeeded;
    }





    public void setInZone()
    {
        //object in win zone
        insideZone = true;

    }

    public void setOutZone()
    {
        //object in win zone
        insideZone = false;

    }


    public void winState()
    {
        if (insideZone == true && distBlocks >= scoreNeeded)
        {
            Debug.Log("You have beat the level");
            //score = 0;
            LoadNextScene();
        }


    }


    public void tryUp()
    {
        numTries = numTries + 1;
        Debug.Log("tries " + numTries);
        Debug.Log("score is " + score);

    }


    public void LoadNextScene()
    {

        level = SceneManager.GetActiveScene().buildIndex - 1;
        Debug.Log("level is " + level);
        //score equation
        if (level == 1)
        {

            score = (distBlocks * 100) + (1000 - (numTries * 5));
            Debug.Log("blk is " + distBlocks + "numof tries " + numTries + "score " + score);
            addScore(id, gameid, score, level);


            leveltime = Time.time;


            
        Debug.Log("score1 is " + score);
        
        Debug.Log("time is " + leveltime);
        
        Debug.Log("tries1 " + numTries);


        }

        if (level == 2)
        {

            float totalTime = Time.time - level;
            int totalSeconds = Mathf.RoundToInt(totalTime);
            score = (distBlocks * 100) + (1000 - (numTries * 5)) - (totalSeconds * 2);
            addScore(id, gameid, score, level);


                 Debug.Log("score1 is 2" + score);
        
        Debug.Log("time is2 " + leveltime);
        
        Debug.Log("tries2 " + numTries);
        }

        if (level == 3)
        {
            difficultyFactor = 2;
            float totalTime = Time.time - startTime;
            int totalSeconds = Mathf.RoundToInt(totalTime);
            score = (distBlocks * 100) + (1000 - (numTries * 5)) - (totalSeconds * 2) - (difficultyFactor * 5);
            addScore(id, gameid, score, level);

                    Debug.Log("score1 is 3" + score);
        
        Debug.Log("time is3 " + leveltime);
        
        Debug.Log("tries3" + numTries);

        }

        if (level == 4)
        {
            difficultyFactor = 3;
            float totalTime = Time.time - startTime;
            int totalSeconds = Mathf.RoundToInt(totalTime);
            score = (distBlocks * 100) + (1000 - (numTries * 5)) - (totalSeconds * 2) - (difficultyFactor * 5);
            addScore(id, gameid, score, level);

        }
        else
        {
            difficultyFactor = 3;
            float totalTime = Time.time - startTime;
            int totalSeconds = Mathf.RoundToInt(totalTime);
            score = (distBlocks * 100) + (1000 - (numTries * 5)) - (totalSeconds * 2) - (difficultyFactor * 5);
            addScore(id, gameid, score, level);
        }

        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("Login_page");
        }
        else
        {
            // load menu
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
       

    }


    // Reload scene
    public void ReloadScene()
    {
        // score = 0;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}