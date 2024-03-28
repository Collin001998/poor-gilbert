using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waves : MonoBehaviour
{
    [System.Serializable]
    public struct WaveInfo
    {
        public int duraction;
        public int coolDownPeriodAfterWave;
        public int amountEnemiesPerSpawn;
        public int duractionBoostMode;
        public int minSecBetweenSpawns;
        public int maxSecBetweenSpawns;
        public bool bossWave;

        public WaveInfo(int wDuraction,int wCoolDownPeriodAfterWave,int wAmountEnemiesPerSpawn,int wDuractionBoostMode,int wMinSecBetweenSpawns,int wMaxSecBetweenSpawns,bool wBosswave)
        {
            duraction = wDuraction;
            coolDownPeriodAfterWave = wCoolDownPeriodAfterWave;
            amountEnemiesPerSpawn = wAmountEnemiesPerSpawn;
            duractionBoostMode = wDuractionBoostMode;
            minSecBetweenSpawns = wMinSecBetweenSpawns;
            maxSecBetweenSpawns = wMaxSecBetweenSpawns;
            bossWave = wBosswave;
    }

    }
    [SerializeField]
    public List<WaveInfo> waves;

    public AudioClip waveSound;
    public AudioClip breakSound;

    public float timer = 90;
    public float coolDownPeriod = 30;
    public int waveCount = 0;
    public bool waveInProgress = false;
    public bool bossWave = false;
    public bool isCooldown = false;


    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveInProgress)
        {
            if(timer > 0)
            {
                timer -= 1 * Time.deltaTime;
            }
            
            if (timer <= 0 && coolDownPeriod > 0)
            {
                coolDownPeriod -= 1 * Time.deltaTime;
                if (!isCooldown)
                {
                    StartCoolDown();
                    isCooldown = true;
                }
                
            }

            if(coolDownPeriod <= 0)
            {
                waveInProgress = false;
                audioSource.clip = breakSound;
            }
            //Debug.Log("wave: " + waveCount+" timer: "+timer + " cooldown: "+ coolDownPeriod);
        }
        
        if (!waveInProgress)
        {
            if(!bossWave && waveCount != waves.Count)
            {
                SartWave(waves[waveCount]);
            }
            
            
        }
        if (bossWave)
        {
            if (GameObject.FindGameObjectsWithTag("enemies").Length <= 0)
            {
                GameObject.DontDestroyOnLoad(GameObject.Find("Score"));
                SceneManager.LoadScene(4);
                
            }

        }
    }
    private void SartWave(WaveInfo wave)
    {
        //
        timer = wave.duraction;
        coolDownPeriod = wave.coolDownPeriodAfterWave;
        waveInProgress = true;
        isCooldown = false;
        bossWave = wave.bossWave;
        //

        audioSource.clip = waveSound;
        
        foreach(GameObject spawn in GameObject.FindGameObjectsWithTag("enemiesSpawn"))
        {
            EnemySpawn spawnScript = spawn.GetComponent<EnemySpawn>();
            spawnScript.SetProperties(wave.minSecBetweenSpawns, wave.maxSecBetweenSpawns, wave.amountEnemiesPerSpawn);
            spawnScript.SwitchSpawn();
        }

        waveCount++;
    }
    private void StartCoolDown()
    {
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("enemiesSpawn"))
        {
            EnemySpawn spawnScript = spawn.GetComponent<EnemySpawn>();
            spawnScript.SwitchSpawn();
        }
    }
    private void ResetTimer()
    {
        
    }
}
