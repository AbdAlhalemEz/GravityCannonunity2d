using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerBehave : MonoBehaviour
{
public int speed = 10;
//public float acceleration = 0;

    public float VelNow=0; 
    //public float VelThen=0;
    //public int count = 1000;
    public float force = 0;
    public float Mass = 0;

    public int jump = 5;
public float moveX;
public Rigidbody2D rb;

   

    //public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        // acceleration = (VelNow - VelThen)/1000;



        //force = acceleration * gameObject.GetComponent<Rigidbody2D>().mass;


        Mass = gameObject.GetComponent<Rigidbody2D>().mass;

        VelNow = rb.velocity.magnitude;

        force = (((VelNow * VelNow) * Mass)/2);

        PlayerMove();
        
        //Debug.Log(onGround);
        if(Input.GetButtonDown ("Jump")){Jumper();}
        //if (onGround == false ){animator.SetBool("Ground",false);} else {animator.SetBool("Ground",true);}
        //Debug.Log(onGround);

       
    }

    void PlayerMove(){

moveX = Input.GetAxis("Horizontal");
if (moveX < 0.0f){ gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed); }
else if (moveX > 0.0f){ gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed); }

//gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

        //animator.SetFloat("speed", Mathf.Abs(moveX * speed));
    }

    void Jumper(){
gameObject.GetComponent<Rigidbody2D>().AddForce (Vector2.up * jump); 
//onGround = false;
    }



    



}
