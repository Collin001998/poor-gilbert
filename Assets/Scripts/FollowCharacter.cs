using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private float rotationSpeed = 180;
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, 0);
    [SerializeField]
    public bool orb = false;
    [SerializeField]
    private bool freeCam = false;

    float rotationX = 0;
    float rotationY = 0;
    float keyRotation;
    Vector3 Targetposition;

    Transform camTarget;
    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;
        if (Target)
        {
            camTarget = Target.Find("camTarget");
            transform.parent = camTarget;
            Targetposition = camTarget.position;
            transform.position = Targetposition + offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
        rotationY += Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        
        if (!freeCam && orb)
        {
            if(Input.GetAxis("Mouse X") == 0)
            {
                rotationX += Time.deltaTime * (rotationSpeed / 2);
            }
            if (rotationY > -50 && rotationY < 50)
            {
                camTarget.rotation = Quaternion.Euler(0, rotationX, rotationY);
            }
            
        }
        if(!freeCam && !orb && Target)
        {
            if (!Input.GetKey(KeyCode.Mouse1))
            {
                Target.rotation = Quaternion.Euler(0, rotationX, 0);
            }
            
            if (rotationY > -50 && rotationY < 30)
            {
                camTarget.rotation = Quaternion.Euler(0, rotationX, rotationY);
            }
        }
        if(!orb && freeCam)
        {
            
        }
       
        
    }
}
