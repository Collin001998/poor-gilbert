using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static ObjectResources;

public class Trap : MonoBehaviour
{
    
    [SerializeField]
    public int UniqueID;
    [SerializeField]
    public bool IsDestroyAble = true;
    [SerializeField]
    public float health = 100;
    [SerializeField]
    public int attackPower = 0;
    [SerializeField]
    public int attackSpeed = 0;
    [SerializeField]
    public Type trapType = Type.blocking;
    [SerializeField]
    public float longRange = 2;
    [SerializeField]
    public float shortRange = 1;
    private GameObject target = null;
    private GameObject closedEnemy = null;
    [SerializeField]
    public TrapNeededResources neededResources = new TrapNeededResources(0, 0, 0);
    private float attackTimer = 0;
    public int slowDown = 10;
    [Range(1, 10)]
    public int nockBackForce = 1;
    private List<GameObject> enemiesWithinRange = new List<GameObject>();

    public enum Type
    {
        long_range,
        short_range,
        blocking,
        slowdown
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trapType == Type.long_range || trapType == Type.short_range)
        {
            if (enemiesWithinRange.Count != 0)
            {
                Vector3 targetPostition = new Vector3(GetClosedEnemy(enemiesWithinRange).transform.position.x,
                                        this.transform.position.y,
                                        GetClosedEnemy(enemiesWithinRange).transform.position.z);
                this.transform.LookAt(targetPostition);
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackSpeed)
                {
                    AttackEnemy(GetClosedEnemy(enemiesWithinRange));
                    attackTimer = 0;
                }

            }
        }
        if(trapType == Type.blocking)
        {

        }
        if(trapType == Type.slowdown)
        {

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (trapType == Type.slowdown)
        {
            if (other.tag == "enemies")
            {
                NavMeshAgent navMeshAgent = other.GetComponent<NavMeshAgent>();
                navMeshAgent.speed /= slowDown;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemies")
        {
            Debug.Log("collided with enemie");
            enemiesWithinRange.Add(other.gameObject);
            //AttackEnemy(other.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "enemies")
        {
            Debug.Log("enemy out of range");
            enemiesWithinRange.Remove(other.gameObject);
        }
    }
    public void DoDamage(int damage)
    {
        if (IsDestroyAble)
        {
            health -= damage;
            Debug.Log("trap health: " + health);
            if (health <= 0)
            {
                BreakObject();
            }
        }
       
    }

    private void BreakObject()
    {
        Destroy(this.gameObject);
    }

    private void AttackEnemy(GameObject enemy)
    {
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (trapType == Type.long_range && IsDestroyAble || trapType == Type.short_range && IsDestroyAble)
        {
           
            enemyScript.DoDamge(this.attackPower, this.gameObject);
        }
        
        if (!enemyScript.target && IsDestroyAble)
        {
            enemyScript.SetTarget(this.gameObject);
        }
    }

    private GameObject GetClosedEnemy(List<GameObject> enemies)
    {
        GameObject closedEnemy = enemies[0];
        foreach(GameObject enemy in enemies)
        {
            if(enemy == null)
            {
                enemiesWithinRange.Remove(enemy);
            }
            else
            {
                if (Vector3.Distance(this.transform.position, enemy.transform.position) < Vector3.Distance(this.transform.position, closedEnemy.transform.position))
                {
                    closedEnemy = enemy;
                }
            }
            
        }
        
        return closedEnemy;
    }
}
