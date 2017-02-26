using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    // Use this for initialization
    public Object parent;

    void OnMouseDown()
    {
        var type = parent.GetType();
        var method = type.GetMethod("OnClick");

        method.Invoke(parent, new object[] { });



    }

}
