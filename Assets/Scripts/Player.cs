using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 100;
    private int waveCount;
    public Text waveText;

    public AudioClip damageSound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        waveCount = 1;
        waveText.text = "Wave " + waveCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoDamage(int damage,GameObject sourceEnemy)
    {
        //lower the health with given damage
        audioSource.PlayOneShot(damageSound, 1);
        Vector3 dir = (sourceEnemy.transform.position - this.transform.position).normalized;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(-dir * 100 * 2);
        health -= damage;
        Debug.Log(this.gameObject.name + " health: "+ health);
        //check if health is lower than or equal to 0, if so kill the enemy
        if (health <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        this.GetComponent<CharacterMovement>().enabled = false;
        SceneManager.LoadScene(3);
        
        //put something here that triggers the gameover stuff
    }
}
