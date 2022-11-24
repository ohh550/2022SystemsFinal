using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int _gatheredAmount = 0;

    public bool Gather()
    {
        if (_gatheredAmount >= 5) return false;
       
        _gatheredAmount++;
        return true;
    }
}
