using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DatabaseReader
{
    public int onoff_value = 1;
    IEnumerator Start()
    {
        // Replace "your_php_script.php" with the filename of your PHP script
        UnityWebRequest www = UnityWebRequest.Get("https://localhost/dashboard/adv/scores/timePlayed.php?do=3");
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
    }
}
