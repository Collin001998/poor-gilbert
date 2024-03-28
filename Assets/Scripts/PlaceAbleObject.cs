using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAbleObject : MonoBehaviour
{
    private bool collides = false;
    MeshRenderer[] meshRenderers;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<MeshRenderer>())
        {
            meshRenderers = this.gameObject.GetComponents<MeshRenderer>();
        }
        else
        {
            Debug.Log("testieeeeessss");
            meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (collides)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material.SetColor("_Color", new Color(1, 0, 0, 0.3f));
            }
           
        }
        else
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material.SetColor("_Color", new Color(0, 1, 0, 0.3f));
            }
            
        }

        
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "traps")
        {
            float dist = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (dist < 0.1)
            {
                collides = true;
            }
            else
            {
                collides = false;
            }
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "traps" || other.gameObject.tag == "enemies")
        {
            float dist = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (dist > 0.1)
            {
                collides = false;
            }
            
        }
    }*/

    public bool CanBePlaced()
    {
        if (collides)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
