using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    // Use this for initialization
    public Object parent;

<<<<<<< HEAD

=======
>>>>>>> JamesBranch
    void OnMouseDown()
    {
        var type = parent.GetType();
        var method = type.GetMethod("OnClick");

        method.Invoke(parent, new object[] { });



    }

}
