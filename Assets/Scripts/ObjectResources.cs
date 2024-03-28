using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResources : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> resources;
    [System.Serializable]
    public struct TrapNeededResources
    {
        public int glue, plastic, stick;
        public TrapNeededResources(int rGlue, int rPlastic, int rStick)
        {
            glue = rGlue;
            plastic = rPlastic;
            stick = rStick;
        }

    }
    public bool ResourceChecker(GameObject trap)
    {
        Trap trapScript = trap.GetComponent<Trap>();
        TrackPoint trackPoint = this.GetComponent<TrackPoint>();
        if(trackPoint.glue >= trapScript.neededResources.glue && trackPoint.plastic >= trapScript.neededResources.plastic && trackPoint.stick >= trapScript.neededResources.stick)
        {
            Debug.Log(trap.name + " can be crafted");
            return true;
        }
        return false;
    }
    public void RemoveResource(TrapNeededResources neededResources)
    {
        TrackPoint trackPoint = this.GetComponent<TrackPoint>();
        trackPoint.glue -= neededResources.glue;
        trackPoint.plastic -= neededResources.plastic;
        trackPoint.stick -= neededResources.stick;
    }

    public GameObject spawnResource()
    {
        int rand = Random.Range(0, 3);
        return resources[rand];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
