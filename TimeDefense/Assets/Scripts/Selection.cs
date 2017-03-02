using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{


    public Tower parent;
    // Update is called once per frame
    void Update()
    {

        if (parent.tag == "Selected")
        {

            GetComponent<SpriteRenderer>().enabled = true;

        }
        else
        {

            GetComponent<SpriteRenderer>().enabled = false;
        }

        transform.localPosition = new Vector3(0, 0.2f * Mathf.Sin(2.0f * Mathf.PI * Time.time) + 1.5f, 0);

    }
}
