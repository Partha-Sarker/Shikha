using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IExecutableObject
{
    [SerializeField]
    private string requiredItem = "Key";
    [SerializeField]
    private GameObject lockedGate, unlockedGate;
    [SerializeField]
    private Collider2D col;

    public bool Execute(string itemName)
    {
        if (itemName == requiredItem)
        {
            lockedGate.SetActive(false);
            unlockedGate.SetActive(true);
            col.enabled = false;
            //Destroy(this.gameObject, 5);
            return true;
        }
        return false;
    }
}
