using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Object_Damage : MonoBehaviour
{
    public float health = 100000;
    public Transform OnePoint;
    public Transform TwoPoint;
    public Rigidbody2D ThisRb;
    public Transform ThisTf;
    private int Dead = 0; 

    public GameObject Damage_UI;
    

    public GameObject smaller;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

       
    }




    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.collider);
         
        if (col.gameObject.tag == "Ball") { 
            //Debug.Log("test");
           // Debug.Log("ThisRB " + ThisRb.velocity.magnitude);
            //Debug.Log(col.gameObject.GetComponent<Ball_Behaviour>().force);
            health = health - col.gameObject.GetComponent<Ball_Behaviour>().force;

            if (col.gameObject.GetComponent<Ball_Behaviour>().force > 50)
            {
                //POP_controller.Create(ThisTf.position, col.gameObject.GetComponent<PlayerBehave>().force);
                GameObject dmgPOP = (GameObject)Instantiate(Damage_UI, ThisTf.position, Quaternion.identity);
                TextMeshPro textMesh = dmgPOP.GetComponent<TextMeshPro>();
                textMesh.SetText(Math.Round(col.gameObject.GetComponent<Ball_Behaviour>().force, 2).ToString());
            }

            if (health <= 0)
            {

                Dead = Dead + 1;

            }
        }

        if (Dead == 1)
        {
            Dead = Dead + 1;
            Invoke("split", 0.25f);
        }

    }

    void split()
    {

        GameObject bGO1 = (GameObject)Instantiate(smaller, OnePoint.position, ThisTf.rotation);
        GameObject bGO2 = (GameObject)Instantiate(smaller, TwoPoint.position, ThisTf.rotation);
        Rigidbody2D bRb1 = bGO1.GetComponent<Rigidbody2D>();
        Rigidbody2D bRb2 = bGO2.GetComponent<Rigidbody2D>();
        bRb1.velocity = ThisRb.velocity;
        bRb2.velocity = ThisRb.velocity;
        bRb1.angularVelocity = ThisRb.angularVelocity;
        bRb2.angularVelocity = ThisRb.angularVelocity;
        //bRb1.AddForce(ThisRb.velocity, ForceMode2D.Impulse);
        //bRb2.AddForce(ThisRb.velocity*2, ForceMode2D.Impulse);
        GameManager.instance.ChangeScore();
        Destroy(gameObject);

    }

    





}
