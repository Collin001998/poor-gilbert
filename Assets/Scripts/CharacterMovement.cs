using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private Transform relativeToCamera;

    [SerializeField]
    [Range(1, 10)]
    private float speed = 1;
    [SerializeField]
    [Range(1, 10)]
    private float runSpeed = 1;
    private float walkSpeed;
    [SerializeField]
    [Range(1, 10)]
    private float jumpSpeed = 1.2F;

    [SerializeField]
    private KeyCode jumpKey;

    [SerializeField]
    private int secondsBeforeIdle = 30;

    private bool canJump = true;
    private Vector2 input;

    bool idle = false;
    float timer;
    Animator Animator;
    
    //unity driven funtions
    void Start()
    {
        walkSpeed = speed;
        Animator = this.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            Animator.SetTrigger("run");
            walkSpeed = speed;
            speed = (runSpeed);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            Animator.SetTrigger("stafe left");
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            Animator.SetTrigger("stafe right");
        }
        else if (Input.GetKey(KeyCode.W))
        {
            speed = walkSpeed;
            Animator.SetTrigger("walk");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Animator.SetTrigger("stafe left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Animator.SetTrigger("stafe right");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Animator.SetTrigger("walk backwards");
        }
        else
        {
            speed = walkSpeed;
            Animator.SetTrigger("idle");
        }
        timer += 1 * Time.deltaTime;
        IdleCheck(timer);
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)
        {
            timer = 0;
        }

        MovePlayerRelativeToCam(relativeToCamera, speed);

        if (Input.GetKeyDown(jumpKey) && canJump)
        {
            PlayerJump(jumpSpeed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "attic" || collision.gameObject.tag == "traps" || collision.gameObject.tag == "clutter")
        {
            canJump = true;
        }
    }

    //object funtions

    private void MovePlayerRelativeToCam(Transform camera, float playerSpeed)
    {
        //get your movement input and put them in 2d raster
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        //check what direction the camera is looking at
        Vector3 camDirection = camera.forward;
        Vector3 camDirectionSides = camera.right;
        camDirectionSides.y = 0;
        camDirection.y = 0;
        camDirection = camDirection.normalized;

        //move towards the direction where the camera is pointing at
        transform.position += (camDirection * input.y + camDirectionSides * input.x) * Time.deltaTime * playerSpeed * 0.1f;
    }

    private void PlayerJump(float jumpSpeed)
    {
        //set jumping to false if player busy jumping and add force to go up in the air
        canJump = false;
        Animator.SetTrigger("jump");
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.up * 100 * jumpSpeed);
    }

    //check if the player is standing still for a x amount of seconds
    private void IdleCheck(float time)
    {
        if(time >= secondsBeforeIdle && !idle)
        {
            idle = true;
            relativeToCamera.GetComponent<FollowCharacter>().orb = true;
        }
        if(time <= secondsBeforeIdle && idle)
        {
            idle = false;
            relativeToCamera.GetComponent<FollowCharacter>().orb = false;
        }
    }
    
}
