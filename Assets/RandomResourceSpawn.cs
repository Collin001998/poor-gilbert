using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomResourceSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject Glue = null;
    [SerializeField]
    private GameObject Plastic = null;
    [SerializeField]
    private GameObject Stick = null;
    [SerializeField]
    public int amountGlue = 2;
    public int amountPlastic = 2;
    public int amountStick = 2;

    // Start is called before the first frame update
    void Start()
    {
        SpawnResourse(amountGlue, Glue);
        SpawnResourse(amountPlastic, Plastic);
        SpawnResourse(amountStick, Stick);
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
            (transform.position.y),
            Random.Range((transform.position.z - collider.radius), (transform.position.z + collider.radius))
            );
            Debug.Log("enemy spawn:" + randomPosition + " minX:" + (transform.position.x - collider.radius));
            return randomPosition;
        }

        return SpawnPoint.position;
    }

    void SpawnResourse(int amount, GameObject gameObject)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemyInstance = Instantiate(gameObject, SpawnPosition(this.transform), gameObject.transform.rotation);
            //enemyInstance.transform.rotation = Quaternion.Euler(-50, 0, 0);
        }
    }
}
