using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlockSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject block = null;
    [SerializeField]
    public int amountPerSpawn = 10;
    // Start is called before the first frame update
    void Start()
    {
        SpawnBlock(amountPerSpawn, block);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log("enemy spawn:" + randomPosition + " minX:" + (transform.position.x - collider.radius));
            return randomPosition;
        }

        return SpawnPoint.position;
    }

    void SpawnBlock(int amount, GameObject gameObject)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemyInstance = Instantiate(gameObject, SpawnPosition(this.transform), Quaternion.identity);
            Trap trap = gameObject.GetComponent<Trap>();
            trap.IsDestroyAble = false;
        }
    }
}
