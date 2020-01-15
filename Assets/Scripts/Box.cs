using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IExecutableObject
{
    public void Execute(string itemName)
    {
        print(itemName + " is breaking the box");
    }
}
