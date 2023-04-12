using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // public InputField playerName;
    public int playerId;
    public int gameId;
    public int score;
    public int level;




    public void AddScoreToDatabase()
    {
        StartCoroutine(AddScore());
    }

    IEnumerator AddScore()
    {
        // Get the URL of the PHP script
        string url = "https://advgamin.000webhostapp.com/timePlayed.php?do=2";

        // Create a form with the name and score parameters
        WWWForm form = new WWWForm();
        //   form.AddField("name", playerName);
        form.AddField("playerId", playerId.ToString());
        form.AddField("gameId", gameId.ToString());
        form.AddField("score", score.ToString());
        form.AddField("level", level.ToString());

        // Send a POST request to the PHP script with the form data
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // Disable SSL verification
        request.certificateHandler = new AcceptAllCertificates();

        yield return request.SendWebRequest();

        // Check if there was an error with the request
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }
}