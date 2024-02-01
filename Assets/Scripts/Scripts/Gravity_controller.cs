using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_controller : MonoBehaviour
{

    public float inputG = -9.8f;

    
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Physics2D.gravity = new Vector2(0, inputG);
    }
}
