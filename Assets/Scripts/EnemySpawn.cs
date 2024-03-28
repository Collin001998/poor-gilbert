using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject enemy = null;
    [SerializeField]
    [Range(0,100)]
    private int chance = 25;
    [SerializeField]
    public bool boostMode = false;
    [SerializeField]
    public bool spawnOn = true;
    [SerializeField]
    public int minSecBetweenSpawns = 3;
    [SerializeField]
    public int maxSecBetweenSpawns = 10;
    [SerializeField]
    public int amountPerSpawn = 1;

    private float timer = 0;
    void Start()
    {
        //creating random position within a surtain radius
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (spawnOn)
        {
            timer += Time.deltaTime;
            if (timer > Random.Range(minSecBetweenSpawns,maxSecBetweenSpawns))
            {
                timer = 0;
                if (boostMode)
                {
                    SpawnEnemy(amountPerSpawn * 2, enemy);
                }
                else
                {
                    SpawnEnemy(amountPerSpawn, enemy);
                }
            }
        }
    }

    Vector3 SpawnPosition(Transform SpawnPoint)
    {
        // TODO: calculate random position within a range of distance
        SphereCollider collider = this.GetComponent<SphereCollider>();
        if (collider)
        {
            Vector3 randomPosition = new Vector3(
            Random.Range((transform.position.x - collider.radius), (transform.position.x + collider.radius)),
            transform.position.y,
            Random.Range((transform.position.z - collider.radius), (transform.position.z + collider.radius))
            );
            Debug.Log("enemy spawn:" + randomPosition  + " minX:" + (transform.position.x - collider.radius));
            return randomPosition;
        }

        return SpawnPoint.position;
    }

    void SpawnEnemy(int amount, GameObject gameObject)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemyInstance = Instantiate(gameObject, SpawnPosition(this.transform), Quaternion.identity);
            Enemy enemyObject = enemyInstance.GetComponent<Enemy>();
            enemyObject.speed = 1;
        }
    }
    public void SetProperties(int minSpawn, int maxSpawn, int amountOfPerSpawn)
    {
        minSecBetweenSpawns = minSpawn;
        maxSecBetweenSpawns = maxSpawn;
        amountPerSpawn = amountOfPerSpawn;
}
    public void SwitchSpawn()
    {
        if (spawnOn)
        {
            Debug.Log("spawn switched off");
            spawnOn = false;
        }
        else
        {
            Debug.Log("spawn switched on");
            spawnOn = true;
        }
    }
    public void SwitchBoostMode()
    {
        if (boostMode)
        {
            boostMode = false;
        }
        else
        {
            boostMode = true;
        }
    }

    
}
