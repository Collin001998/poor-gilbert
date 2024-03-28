using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int speed = 1;
    public int health = 100;
    public int attackPower = 10;
    public float attackSpeed = 3;
    public int addScore = 100;
    private GameObject player = null;
    private float attackTimer = 0;
    public GameObject target = null;
    private bool tragetLocationSet = false;
    [SerializeField]
    [Range(0,100)]
    public int chance = 100;
    private NavMeshAgent navMeshAgent;
    public AudioClip spawnSound;
    public AudioClip damageSound;
    AudioSource audioSource;

    private float cooldown = 3;
    private bool canAttack = true;

    UI UI;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnSound,1);
        player = GameObject.Find("Character");
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        UI = player.gameObject.GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player && !target)
        {
            //transform.LookAt(player.transform);
            //Vector3 distance = player.transform.position - transform.position;
            //transform.position += distance * Time.deltaTime * speed;
            navMeshAgent.SetDestination(player.transform.position);
        }
        if (target)
        {
            this.transform.LookAt(target.transform);
            //Vector3 distance = target.transform.position - transform.position;
            //transform.position += distance * Time.deltaTime * 0.4f;
            if (!tragetLocationSet)
            {
                tragetLocationSet = true;
                navMeshAgent.SetDestination(target.transform.position);
            }
            
        }
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "traps")
        {
            if (canAttack)
            {
                AttackTrap(collision.gameObject);
                cooldown = 3;
                canAttack = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Character")
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                AttackPlayer(other.gameObject);
                attackTimer = 0;
            }

        }
        if (other.gameObject.tag == "traps")
        {
            float dist = Vector3.Distance(other.gameObject.transform.position, transform.position);
            if (dist < 0.1)
            {
                if (canAttack)
                {
                    AttackTrap(other.gameObject);
                    cooldown = 3;
                    canAttack = false;
                }
            }
                
        }
        /*if (other.gameObject.tag =="traps")
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                AttackTrap(other.gameObject);
                attackTimer = 0;
            }

        }*/
    }
    public void AttackPlayer(GameObject player)
    {
        // TODO: play attack animation
        // TODO: do damage to player met the attack power
        Debug.Log("hit a player, now i'm a badass toy");
        Player playerScript = player.GetComponent<Player>();
        playerScript.DoDamage(attackPower,this.gameObject);
    }
    public void AttackTrap(GameObject trap)
    {
        Debug.Log("hit a trap, now i'm a badass toy");
        Trap trapScript = trap.GetComponent<Trap>();
        trapScript.DoDamage(attackPower);
    }
    public void DoDamge(int damage, GameObject trap)
    {
        //lower the health with given damage
        audioSource.PlayOneShot(damageSound, 1);
        Vector3 dir = (trap.transform.position - this.transform.position).normalized;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(-dir * 100 * trap.GetComponent<Trap>().nockBackForce);
        health -= damage;
        Debug.Log(this.gameObject.name + " health: " + health);
        //check if health is lower than or equal to 0, if so kill the enemy
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        // TODO: spawn/drop resources
        GameObject resource = player.GetComponent<ObjectResources>().spawnResource();
        GameObject resourceInstance = Instantiate(resource, this.transform.position, resource.transform.rotation);

        // TODO: create a cloud or play someother effect
        UI.myScore += addScore;
        // TODO: destory enemy object
        Destroy(this.gameObject);
    }

    bool CheckChance(int chance)
    {
        int random = Random.Range(1, 101);
        if(random <= chance)
        {
            return true;
        }
        return false;
    }

    void AttackObject(GameObject trap)
    {
        player = null;

        Trap trapScript = trap.GetComponent<Trap>();
        trapScript.DoDamage(this.attackPower);
        // TODO: Play attack animation
        // TODO: Do damage to the trap if the the trap type can be destoryed
    }

    public void SetTarget(GameObject trap)
    {
        if (CheckChance(chance))
        {
            if (!target)
            {
                target = trap;
            }
            
        }
        
    }
}
