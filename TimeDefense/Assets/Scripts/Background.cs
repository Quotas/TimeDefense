using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    public Tower curSelected;

    // Use this for initialization

    void OnMouseDown()
    {

        if (curSelected != null)
        {


            Debug.Log("Deslected", curSelected);
            curSelected.Deselect();


            curSelected = null;


        }

    }
}
