using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Net;
using System;

public class Login_script : MonoBehaviour
{
    [SerializeField] public TMP_InputField m_user;
    [SerializeField] public TMP_InputField m_pass;
    [SerializeField] public TextMeshProUGUI m_alert;
    public Button m_Submit;
    public Button m_Scores;

    public int onoff_value = 0;

    private string phpURL = "https://advgamin.000webhostapp.com/login.php";

    public static int id = -1;// public variable to store the ID on successful login

    void TaskOnClick()
    {
        string userName = m_user.text;
        string passWord = m_pass.text;

        // Create a WWWForm with the username and password fields
        WWWForm form = new WWWForm();
        form.AddField("username", userName);
        form.AddField("password", passWord);

        // Send an HTTP POST request to the PHP script
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

                Debug.Log("rrrrrrrrrrr" + responseText);

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
                m_alert.text = "Invalid username or password.";
                m_alert.enabled = true;
                Debug.Log("Invalid username or password.");
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
