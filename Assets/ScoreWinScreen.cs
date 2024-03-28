using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWinScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    PlayerScore scoreObject;
    void Start()
    {
        scoreObject = GameObject.Find("Score").GetComponent<PlayerScore>();
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + scoreObject.score.ToString();
        
    }
}
