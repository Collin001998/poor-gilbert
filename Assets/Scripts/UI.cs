using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    public Text waveText;
    public Text timerText;
    public Text plastic;
    public Text glue;
    public Text stick;
    public Text score;

    public Image image;

    public Sprite gilbertStageOne;
    public Sprite gilbertStageTwo;
    public Sprite gilbertStageThree;
    public Sprite gilbertStageFour;
    public Sprite gilbertStageFive;
    public Sprite gilbertStageSix;

    public Text TrapCount1;
    public Text TrapCount2;
    public Text TrapCount3;

    public int TrapCountInt1;
    public int TrapCountInt2;
    public int TrapCountInt3;

    public Image TrapTumb1;
    public Image TrapTumb2;
    public Image TrapTumb3;

    public Image TrapHelp1;
    public Image TrapHelp2;
    public Image TrapHelp3;

    public Slider craftSlider;

    public int myScore = 0;

    PlacingObjects placingObjects;
    TrackPoint trackPointScript;
    Player playerScript;
    Waves wavesScript;
    // Start is called before the first frame update
    void Start()
    {
        placingObjects = this.GetComponent<PlacingObjects>();
        trackPointScript = this.GetComponent<TrackPoint>();
        playerScript = this.GetComponent<Player>();
        wavesScript = this.GetComponent<Waves>();
        TrapTumb1.color = new Color(1, 1, 1, 0.3f);
        TrapTumb2.color = new Color(1, 1, 1, 0.3f);
        TrapTumb3.color = new Color(1, 1, 1, 0.3f);

        TrapHelp1.color = new Color(1, 1, 1, 0.0f);
        TrapHelp2.color = new Color(1, 1, 1, 0.0f);
        TrapHelp3.color = new Color(1, 1, 1, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("i'm here boii");
            TrapHelp1.color = new Color(1, 1, 1,1f);
            TrapHelp2.color = new Color(1, 1, 1, 0f);
            TrapHelp3.color = new Color(1, 1, 1, 0f);
        } else if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha2))
        {
            TrapHelp1.color = new Color(1, 1, 1, 0f);
            TrapHelp2.color = new Color(1, 1, 1, 1f);
            TrapHelp3.color = new Color(1, 1, 1, 0f);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha3))
        {
            TrapHelp1.color = new Color(1, 1, 1, 0f);
            TrapHelp2.color = new Color(1, 1, 1, 0f);
            TrapHelp3.color = new Color(1, 1, 1, 1f);
        }
        else
        {
            TrapHelp1.color = new Color(1, 1, 1, 0f);
            TrapHelp2.color = new Color(1, 1, 1, 0f);
            TrapHelp3.color = new Color(1, 1, 1, 0f);
        }
        if(TrapCountInt1 <= 0)
        {
            TrapTumb1.color = new Color(1, 1, 1, 0.3f);
        }
        if (TrapCountInt2 <= 0)
        {
            TrapTumb2.color = new Color(1, 1, 1, 0.3f);
        }
        if (TrapCountInt3 <= 0)
        {
            TrapTumb3.color = new Color(1, 1, 1, 0.3f);
        }
        Debug.Log("time remaining: "+ placingObjects.buildTimeRemaining +"time to craft:"+ placingObjects.buildTime);
        if(placingObjects.buildTimeRemaining < placingObjects.buildTime)
        {
            float progressLeft = placingObjects.buildTimeRemaining / placingObjects.buildTime;
            float progressDone = 1f - progressLeft;
            craftSlider.value = progressDone;
        }
        else
        {
            craftSlider.value = 0;
        }
        plastic.text = trackPointScript.plastic.ToString();
        glue.text = trackPointScript.glue.ToString();
        stick.text =  trackPointScript.stick.ToString();

        score.text = "Score: " + myScore.ToString();

        if (wavesScript.bossWave)
        {
            waveText.text = "Final Wave";
        }
        else
        {
            waveText.text = "Wave " + wavesScript.waveCount;
        }
        
        if(wavesScript.timer > 0)
        {
            int time = (int)Math.Round(wavesScript.timer);
            timerText.text = time.ToString();
        }
        if (wavesScript.timer < 0)
        {
            waveText.text = "Break";
            int time = (int)Math.Round(wavesScript.coolDownPeriod);
            timerText.text = time.ToString();
        }

        TrapCount1.text = TrapCountInt1.ToString();
        TrapCount2.text = TrapCountInt2.ToString();
        TrapCount3.text = TrapCountInt3.ToString();

        if(TrapCountInt1 > 0)
        {
            TrapTumb1.color = new Color(1, 1, 1,1f);
        }
        if (TrapCountInt2 > 0)
        {
            TrapTumb2.color = new Color(1, 1, 1, 1f);
        }
        if (TrapCountInt3 > 0)
        {
            TrapTumb3.color = new Color(1, 1, 1, 1f);
        }


        if (playerScript.health < 101 && playerScript.health > 85)
        {
            image.sprite = gilbertStageOne;
        }
        else if (playerScript.health < 84 && playerScript.health > 68)
        {
            image.sprite = gilbertStageTwo;
        }
        else if (playerScript.health < 67 && playerScript.health > 52)
        {
            image.sprite = gilbertStageThree;
        }
        else if (playerScript.health < 51 && playerScript.health > 34)
        {
            image.sprite = gilbertStageFour;
        }
        else if (playerScript.health < 34 && playerScript.health > 18)
        {
            image.sprite = gilbertStageFive;
        }
        else if (playerScript.health < 17 && playerScript.health > 0)
        {
            image.sprite = gilbertStageSix;
        }
    }
}
