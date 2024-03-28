using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MoveToMenu : MonoBehaviour
{

    public VideoPlayer vp;

    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        vp.loopPointReached += EndReached;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndReached(VideoPlayer vp)
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
