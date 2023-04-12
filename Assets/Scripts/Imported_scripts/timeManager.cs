using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class timeManager : MonoBehaviour
{
    public int playerId;
    public int gameId;
    public int time_spent;
    // public InputField last_played;

    public void AddTimeToDatabase()
    {
        StartCoroutine(AddTime());
    }

    IEnumerator AddTime()
    {
        // Get the URL of the PHP script
        string url = "https://advgamin.000webhostapp.com/timePlayed.php?do=1";

        // Create a form with the player ID, game ID, time spent, and last played parameters
        WWWForm form = new WWWForm();

        form.AddField("playerId", playerId.ToString());
        form.AddField("gameId", gameId.ToString());
        form.AddField("time_spent", time_spent.ToString());
        //form.AddField("last_played", last_played.text);

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

public class AcceptAllCertificates : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}
