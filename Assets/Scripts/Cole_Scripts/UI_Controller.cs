using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI m_Vel;
    [SerializeField] public TextMeshProUGUI m_Angle;
    [SerializeField] public TextMeshProUGUI m_Mass;
    [SerializeField] public TextMeshProUGUI m_Gravity;
    [SerializeField] public TextMeshProUGUI m_Wind;
    [SerializeField] public TextMeshProUGUI m_Dist;
    [SerializeField] public TextMeshProUGUI m_LF;
    [SerializeField] public TextMeshProUGUI m_SC;
    [SerializeField] public TextMeshProUGUI m_AD;
    [SerializeField] public TextMeshProUGUI m_T;


    public GameObject Ball;
    public GameObject Wind;
    public GameObject Gravity_Controller;
    public GameObject Cannon;

    private Camera mCam; // Main Camera
    private Vector3 mousePos;

    float dist = 0.0f;
    float time = 180.0f;

    float mathAngle = 0;

    string windDirection = "East";


    // Start is called before the first frame update
    void Start()
    {
        mCam = Camera.main;
        // Ball = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {

        mousePos = mCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        dist = Vector3.Distance(Cannon.transform.position, mousePos);


        if (GameObject.FindGameObjectWithTag("Ball") != null)
        {
            Ball = GameObject.FindGameObjectWithTag("Ball");
            //dist = Vector3.Distance(Cannon.transform.position, Ball.transform.position);
        }
        else
        {
           // dist = 0.0f;
        }
        
        

        //Mass.gameObject.GetComponent<Text>().text = ("Mass: " + Ball.GetComponent<PlayerBehave>().Mass);

        //Velocity.gameObject.GetComponent<Text>().text = ("Velocity: " + Ball.GetComponent<PlayerBehave>().VelNow); 

       

        m_Vel.text = "Velocity: " + Math.Round(Ball.GetComponent<Ball_Behaviour>().VelNow, 2);

        m_Dist.text = "Distance: " + Math.Round(dist,2);

        //mathAngle = Cannon.transform.localEulerAngles.z + 90;

        //transform.rotation.ToAngleAxis(out angle, out axis);

        if (Cannon.transform.localEulerAngles.z > 270f && Cannon.transform.localEulerAngles.z < 360f)
         {
        mathAngle = Cannon.transform.localEulerAngles.z - 270;
         }
         else
        {
             mathAngle = Cannon.transform.localEulerAngles.z + 90;
        }


        m_Angle.text = "Angle: " + Math.Round(mathAngle,2); 

        m_Mass.text = "Mass: " + GameManager.instance.getMass();

        m_Gravity.text = "Gravity: " + Gravity_Controller.GetComponent<Gravity_controller>().inputG;

        m_LF.text = "Launch Force: " + Cannon.GetComponent<CannonController>().ForceValue;

        if (Wind.transform.localEulerAngles.z == 90)
        {
            windDirection = "West";
        }
        else
        {
            windDirection = "East";
        }

        m_Wind.text = "Wind Direction: " + Wind.GetComponent<AreaEffector2D>().forceMagnitude + " km/h " + windDirection;

        m_SC.text = "Shot Count: " + GameManager.instance.getTries();

        m_AD.text = "Amount Destroyed: " + GameManager.instance.getAD() + " out of " + GameManager.instance.getSN();


        time = time - Time.deltaTime;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        m_T.text = "Time Left: "+ string.Format("{0:00}:{1:00}", minutes, seconds);

        if (time <= 0) { GameManager.instance.LoadNextScene(); }

        //+ Ball.GetComponent<PlayerBehave>().VelNow);

        //Gravity.gameObject.GetComponent<Text>().text = ("Gravity: " + Ball.GetComponent<PlayerBehave>().);
    }
}
