using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_object : MonoBehaviour
{
    public float health = 100000;
    
    

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

        if (col.gameObject.tag == "Ball")
        {
           // Debug.Log("test");

           // Debug.Log(col.gameObject.GetComponent<PlayerBehave>().force);
            health = health - col.gameObject.GetComponent<Ball_Behaviour>().force;

            //int mass = col.gameObject.GetComponent<Rigidbody2D>().mass;

        
        
        }

        if (health <= 0)
        {
            //AudioSource.PlayClipAtPoint(die, transform.position, 1);
            //Debug.Log("dead");
            Destroy(gameObject);
        }
        // swap();
    }



}
