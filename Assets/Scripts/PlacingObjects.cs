using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingObjects : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> traps;

    public bool holdsInHand = false;
    public bool canBePlaced = true;

    private MeshRenderer meshRenderer;
    public bool placeMode = false;
    private GameObject trapInstance = null;
    private float mouseRotation;
    public float buildTime;
    public float buildTimeRemaining;
    private bool clickedOnce = true;


    private MeshRenderer[] meshRenderers;
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = this.GetComponent<Inventory>();
        buildTimeRemaining = buildTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && placeMode)
        {
            ExitPlaceMode();
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (clickedOnce)
            {
                clickedOnce = false;
                if (trapInstance)
                {
                    Destroy(trapInstance.gameObject);

                    trapInstance = null;
                    meshRenderer = null;
                    placeMode = false;
                }
                GetObjectFromInventory(traps[0].gameObject);
            }
            buildTimeRemaining -= Time.deltaTime;
            Debug.Log(buildTimeRemaining);
            if (buildTimeRemaining < 0)
            {
                inventory.CraftTrap(traps[0].gameObject);
                buildTimeRemaining = buildTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            buildTimeRemaining = buildTime;
            clickedOnce = true;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (clickedOnce)
            {
                clickedOnce = false;
                if (trapInstance)
                {
                    Destroy(trapInstance.gameObject);
                    //reset to default settings
                    trapInstance = null;
                    meshRenderer = null;
                    placeMode = false;
                }
                GetObjectFromInventory(traps[1].gameObject);
            }
            buildTimeRemaining -= Time.deltaTime;
            Debug.Log(buildTimeRemaining);
            if (buildTimeRemaining < 0)
            {
                inventory.CraftTrap(traps[1].gameObject);
                buildTimeRemaining = buildTime;
                Debug.Log("trap build");
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            buildTimeRemaining = buildTime;
            clickedOnce = true;
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (clickedOnce)
            {
                Debug.Log("i'm here");
                clickedOnce = false;
                if (trapInstance)
                {
                    Destroy(trapInstance.gameObject);
                    //reset to default settings
                    trapInstance = null;
                    meshRenderer = null;
                    placeMode = false;
                }
                GetObjectFromInventory(traps[2].gameObject);
            }
            buildTimeRemaining -= Time.deltaTime;
            Debug.Log(buildTimeRemaining);
            if (buildTimeRemaining < 0)
            {
                inventory.CraftTrap(traps[2].gameObject);
                buildTimeRemaining = buildTime;
                Debug.Log("trap build");
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            buildTimeRemaining = buildTime;
            clickedOnce = true;
        }


        if (placeMode)
        {
            
            trapInstance.transform.position = this.transform.Find("objectTarget").position;
            mouseRotation += Input.mouseScrollDelta.y * Time.deltaTime * 100;
            Quaternion rotation = transform.rotation * Quaternion.Euler(0, mouseRotation, 0);
            trapInstance.transform.rotation = rotation;
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return))
            {
                if (trapInstance.GetComponent<PlaceAbleObject>().CanBePlaced())
                {
                    PlaceObject(trapInstance);
                }
            }
        }
    }

    private void GetObjectFromInventory(GameObject trap)
    {
        Inventory inventory = this.GetComponent<Inventory>();
        if (inventory.InventoryChecker(trap))
        {
            
            //create new instance
            trapInstance = Instantiate(trap, SpawnPosition(), Quaternion.identity);
            //meshRenderer = trapInstance.GetComponent<MeshRenderer>();
            //meshRenderer.material.SetColor("_Color", new Color(0, 1, 0, 0.3f));
            
            //switch scripts
            trapInstance.GetComponent<Trap>().IsDestroyAble = false;
            trapInstance.GetComponent<Trap>().enabled = false;
            trapInstance.GetComponent<PlaceAbleObject>().enabled = true;
            
            //switch off components
            trapInstance.GetComponent<Rigidbody>().isKinematic = true;
            trapInstance.GetComponent<BoxCollider>().isTrigger = true;
            //trapInstance.GetComponent<BoxCollider>().enabled = false;
            trapInstance.GetComponent<SphereCollider>().enabled = false;

            //switch the place mode on so system knows that you are ready to place objects
            placeMode = true;
        }/*
        else
        {
            inventory.CraftTrap(trap);
        }
        */
    }
    private void PlaceObject(GameObject trap)
    {
        Inventory inventory = this.GetComponent<Inventory>();
        inventory.RemoveTrapFromInventory(trap);
        trap.GetComponent<PlaceAbleObject>().enabled = false;
        trapInstance.GetComponent<Trap>().IsDestroyAble = true;
        trap.GetComponent<Trap>().enabled = true;
        trap.GetComponent<Rigidbody>().detectCollisions = true;
        trap.GetComponent<Rigidbody>().isKinematic = false;
        trapInstance.GetComponent<BoxCollider>().isTrigger = false;
        trapInstance.GetComponent<SphereCollider>().enabled = true;

        
        //set normal texture back
        if (trap.GetComponent<MeshRenderer>() != null)
        {
            meshRenderers = trap.gameObject.GetComponents<MeshRenderer>();
        }
        else
        {
            meshRenderers = trap.GetComponentsInChildren<MeshRenderer>();
        }

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.SetColor("_Color", new Color(1, 1, 1, 1f));
        }
        // meshRenderer = trap.GetComponent<MeshRenderer>();
        // meshRenderer.material.SetColor("_Color", new Color(1, 1, 1, 1f));

        //reset to default settings
        trapInstance = null;
        meshRenderer = null;
        placeMode = false;
    }
    
    private Vector3 SpawnPosition()
    {
        if (!holdsInHand)
        {
            //get the position from mid screen

        }
        else
        {
            //get the position of the child empty object "holdObjectSpot" and use that as the position
            Transform objectTagert = this.transform.Find("objectTarget");
            return objectTagert.position;
        }
        return this.transform.position;
    }

    private void ExitPlaceMode()
    {
        if (trapInstance)
        {
            Destroy(trapInstance.gameObject);
        }
        
        //reset to default settings
        trapInstance = null;
        meshRenderer = null;
        placeMode = false;
    }
}
