using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Login_script : MonoBehaviour
{
    [SerializeField] public TMP_InputField m_user;
    [SerializeField] public TMP_InputField m_pass;
    [SerializeField] public TextMeshProUGUI m_alert;
    public Button m_Submit;
    public Button m_Scores;

    public int onoff_value = 1;

    private string phpURL = "https://advgamin.000webhostapp.com/login.php";

    public static int id = -1;

    private string userName;  // Declare username at class level
    private string passWord;  // Declare password at class level

    void TaskOnClick()
    {
        userName = m_user.text;
        passWord = m_pass.text;

        WWWForm form = new WWWForm();
        form.AddField("username", userName);
        form.AddField("password", passWord);

        StartCoroutine(SendPostRequest(form));
    }

    void TaskOnClick2()
    {
        SceneManager.LoadScene("High_scores");
    }


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

        //uncomment to be able to let the admin turn game on and off
/*
        if (onoff_value == 0)
        {
            Debug.LogError("Variable v is not set correctly. Quitting the application.");
            //Application.Quit();
            m_alert.enabled = true;
            m_user.enabled = false;
            m_pass.enabled = false;
            m_Submit.enabled = false;

        }
        else
        {
            m_alert.text = "Game is online";
            m_alert.enabled = true;
        }
*/


    }

 IEnumerator SendPostRequest(WWWForm form)
{
    // Send the HTTP POST request and wait for a response
    using (UnityWebRequest www = UnityWebRequest.Post(phpURL, form))
    {
        // Disable SSL verification
        www.certificateHandler = new AcceptAllCertificates();

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("HTTP POST request failed: " + www.error);
            yield break;
        }

        // Handle the response
        if (www.downloadHandler.text != "Invalid username or password.")
        {
            Debug.Log("Login successful!");

            // Get the id from the response
            string responseText = www.downloadHandler.text;

            int index = responseText.IndexOf("!") + 1;
            string idString = responseText.Substring(index).Trim();
            int id = int.Parse(idString);
            Debug.Log("id in log in :" + idString);
            PlayerPrefs.SetInt("MyId", id);
            PlayerPrefs.Save();

            // Try to parse the id as an integer and store it in the public variable
            if (int.TryParse(idString, out id))
            {
                SceneManager.LoadScene("Demo_Level_1");
            }
            else
            {
                m_alert.text = "Error parsing ID from response.";
                m_alert.enabled = true;
                Debug.Log("Error parsing ID from response.");
            }
        }
        else
        {
            // Check if the entered credentials are for the admin account
            if (userName == "admin" && passWord == "admin")
            {
                m_alert.text = "Invalid username or password. You can use 'admin' as both username and password to enter.";
                m_alert.enabled = true;
                SceneManager.LoadScene("Demo_Level_1");
            }
            else
            {
                m_alert.text = "Invalid username or password. You can use 'admin' as both username and password to enter..";
                m_alert.enabled = true;
                Debug.Log("Invalid username or password. You can use 'admin' as both username and password to enter.");
            }
        }
    }
}


    // Start is called before the first frame update
    void Start()
    {
        m_alert.enabled = false;
        StartCoroutine(FetchData());
        m_Submit.onClick.AddListener(TaskOnClick);
        m_Scores.onClick.AddListener(TaskOnClick2);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDestroy()
    {

    }
}
