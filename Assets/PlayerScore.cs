using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    UI uI;
    public int score;
    void Start()
    {
        uI = GameObject.Find("Character").GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        score = uI.myScore;
    }
}
