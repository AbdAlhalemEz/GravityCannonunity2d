using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Behaviour : MonoBehaviour
{
    public float VelNow = 0;
    public float force = 0;
    public float Mass = 0;
    public Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }


        Mass = rb.mass;

        VelNow = rb.velocity.magnitude;

        force = (((VelNow * VelNow) * Mass) / 2);


    }


    

}
