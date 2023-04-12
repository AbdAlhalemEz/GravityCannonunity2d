using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonController : MonoBehaviour
{
    [SerializeField] GameObject barrel; // Sprite Hardpoint
    [SerializeField] GameObject cannonballPrefab; // Projectile Prefab
    [SerializeField] GameObject cannonballPrefab2; // Projectile Prefab
    [SerializeField] GameObject cannonballPrefab3; // Projectile Prefab
    [SerializeField] Transform spawnPoint; // Projectile Instantiaton Point
    [SerializeField] float minAngle, maxAngle; // Min and Max Angles(z-axis)
    [SerializeField] Vector3 initialVelocity; // Initial Veloctiy
    private Camera mCam; // Main Camera
    private float angle; // Angle
    private Vector3 mousePos; // Mouse Postition
    private Vector3 neutralDir; // Neutral direction

    public float ForceValue = 200;
    public float MaxForce = 300;

    private GameObject loadedBall;


    // Awake is called on script load 
    void Awake()
    {
        mCam = Camera.main;
        loadedBall = cannonballPrefab;
    }
    // Update is called once per frame
    void Update()
    {
        AimAtMouse();
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        ChangeForce();

        if (Input.GetKeyDown(KeyCode.R))
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            Debug.Log("loaded 1");
            loadedBall = cannonballPrefab;
           GameManager.instance.setMass(0);
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            Debug.Log("loaded 2");
            loadedBall = cannonballPrefab2;
            GameManager.instance.setMass(1);
        }


        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            Debug.Log("loaded 3");
            loadedBall = cannonballPrefab3;
            GameManager.instance.setMass(2);
        }


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            Debug.Log("loaded 4");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            SceneManager.LoadScene("Login_page");
        }


    }

    // Aims at mouse position
    public void AimAtMouse()
    {
        // Set mouse position to its position in the camera world space
        mousePos = mCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        neutralDir = transform.up;

        // Flatten z axis
        mousePos.z = 0;

        // Calculate signed angle between vectors
        angle = Vector3.SignedAngle(neutralDir, mousePos, Vector3.forward);

        //minAngle = -90f;
        //maxAngle = 0f;

        // Locks position to set angle
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        // Rotate neutral dir by the clamped angle
        mousePos = Quaternion.AngleAxis(angle, Vector3.forward) * neutralDir;

        // Set the rotation so that local up points in mouse direction 
        // and local forward in global forward
        barrel.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos);
        initialVelocity = mousePos;
            //+ spawnPoint.position;
    }

    // Creates and fires a projectile
    public void Fire()
    {
        GameManager.instance.tryUp();
        GameObject cannonball = Instantiate(loadedBall, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = cannonball.GetComponent<Rigidbody2D>();
        rb.AddForce(initialVelocity * ForceValue, ForceMode2D.Impulse);
    }


    void ChangeForce()
    {

        ForceValue += Input.mouseScrollDelta.y;

        if (Input.GetKeyDown(KeyCode.DownArrow)) { ForceValue = ForceValue - 1; }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) { ForceValue = ForceValue + 1; }

        if (ForceValue < 0) { ForceValue = 0;}
        if (ForceValue > MaxForce) { ForceValue = MaxForce; }

    }

    }
