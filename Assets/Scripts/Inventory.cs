using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> traps= new List<int>();
    UI UI;
    void Start()
    {
        UI = this.GetComponent<UI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CraftTrap(GameObject trap)
    {
        Trap trapScript = trap.GetComponent<Trap>();
        Debug.Log(trapScript.name);
        ObjectResources objectResources = this.GetComponent<ObjectResources>();
        if (objectResources.ResourceChecker(trap))
        {
            objectResources.RemoveResource(trapScript.neededResources);
            AddToInventory(trap);
        }
        else
        {
            Debug.LogWarning("sorry you don't have enough resources to build this trap");
        }
    }
    public void AddToInventory(GameObject trap)
    {
        traps.Add(trap.GetComponent<Trap>().UniqueID);
        if (trap.name.Contains("Gilbert_Scorpion_Ballista"))
        {
            UI.TrapCountInt1 += 1;
        }
        if (trap.name.Contains("fidget"))
        {
            UI.TrapCountInt2 += 1;
        }
        if (trap.name.Contains("Letter Block"))
        {
            UI.TrapCountInt3 += 1;
        }

        Debug.Log(traps);
    }
    public void RemoveTrapFromInventory(GameObject trap)
    {
        Trap trapScript = trap.GetComponent<Trap>();
        if (trap.name.Contains("Gilbert_Scorpion_Ballista"))
        {
            UI.TrapCountInt1 -= 1;
        }
        if (trap.name.Contains("fidget"))
        {
            UI.TrapCountInt2 -= 1;
        }
        if (trap.name.Contains("Letter Block"))
        {
            UI.TrapCountInt3 -= 1;
        }
        traps.Remove(trapScript.UniqueID);
    }
    public bool InventoryChecker (GameObject trap)
    {
        Trap trapScript = trap.GetComponent<Trap>();
        foreach (int trapI in traps)
        {
            if (trapI == trapScript.UniqueID)
           {
                Debug.Log("yay " + trap.name + " found in the inventory.");
                return true;
           }
        }
        Debug.Log("sorry "+ trap.name+" not in the inventory.");
        return false; 

    }
}
