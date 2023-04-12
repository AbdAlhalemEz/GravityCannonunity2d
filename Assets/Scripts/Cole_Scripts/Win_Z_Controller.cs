using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_Z_Controller : MonoBehaviour
{
    private float timer = 3.0f;


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            timer = 3.0f;
            Debug.Log("Out Zone");
            GameManager.instance.setOutZone();
        }
    }


    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Ball")
        {
            timer -= Time.deltaTime;
            //Debug.Log("In Zone");
            
        }

        if (timer < 0)
        {
            GameManager.instance.setInZone();
            Debug.Log("Winner");
        }



    }

}
