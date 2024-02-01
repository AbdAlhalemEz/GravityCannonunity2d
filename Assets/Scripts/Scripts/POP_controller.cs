using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class POP_controller : MonoBehaviour
{

    private float lifeTime = 2.0f;
    public GameObject Damage_UI;
    private Color textColor;
    private TextMeshPro textMesh;


    // Start is called before the first frame update
    void Start()
    {
        textMesh = Damage_UI.GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }

    // Update is called once per frame
    void Update()
    {
        float moveUp = 1.0f;
        Damage_UI.transform.position += new Vector3(0, moveUp) * Time.deltaTime;


        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            float disappearTime = 3.0f;
            textColor.a -= disappearTime * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }

        }
    }



}
