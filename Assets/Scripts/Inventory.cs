using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public static Dictionary<String, int> inventory;

    void Start()
    {
        inventory = new Dictionary<string, int>();
        inventory.Add("yarn", 0);
    }
}
