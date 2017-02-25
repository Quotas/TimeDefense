using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public int order;


    private class sort : IComparer<Node>
    {
        int IComparer<Node>.Compare(Node _objA, Node _objB)
        {
            int t1 = _objA.order;
            int t2 = _objB.order;
            return t1.CompareTo(t2);
        }
    }
}
