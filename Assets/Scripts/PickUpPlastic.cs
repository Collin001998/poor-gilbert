using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPlastic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        if(other.name =="Character"){
            other.GetComponent<TrackPoint>().plastic+=2;
            Destroy(gameObject);
        }    
    }
}
