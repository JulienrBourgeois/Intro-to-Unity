using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public float currSpeed;
    public float walkSpeed;
    public float frontSpeed;
    public float jumpForce;
    public bool collidingEnemy = false;

    [Header("Audio")]
    public AudioClip stepSound;
    private int stepCount;
    private int lastAnim;
    public AudioClip jumpSound;
    public AudioClip fallingSound;
    public AudioClip attackSound;
    public AudioClip crouchSound;

    

    [Header("Bools")]
    public bool isSprinting;
    public bool isGrounded=false;
    public bool justJumped=false;
    private float lastJumpTime;
    public bool isCrouched=false;
    public bool attacking=true;
    public bool falling=false;
    private bool fellOffMap = false;

    [Header("Rotes")]
    private Vector2 turn;
    private float xSense=15.0f;

    [Header("Components")]
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public LayerMask layerMask;
    private WorldLoad worldLoad;



    [Header("Collisions")]
    public MeshCollider playerCol;
    public CapsuleCollider playerBlock;

    [Header("Shooting")]
    public GameObject crossHair;


    void Start()
    {
        playerRb=GetComponent<Rigidbody>();
        playerAnim=GetComponent<Animator>();
        playerAudio=GetComponent<AudioSource>();


        MouseLock();

        worldLoad = GameObject.Find("WorldLoad").GetComponent<WorldLoad>();

		Physics.IgnoreCollision(playerCol, playerBlock, true);

        crossHair = GameObject.Find("Canvas").transform.GetChild(2).transform.gameObject;

    }

    void Update()
    {


        if(transform.position.y<-100f)
        {
            transform.position = new Vector3(128f,100f,128f);
            fellOffMap = true;
        }
	
	
        float verticalInput= Input.GetAxis("Vertical");
		float horizontalInput= Input.GetAxis("Horizontal");


        CalculateMovements(horizontalInput, verticalInput);

        CalculateAnimations(horizontalInput, verticalInput);

        if(justJumped)
        {
            if(Time.time- lastJumpTime>0.5f)
            {
                justJumped=false;
            }
        }

        //horizontal turning
        Rotations();

        crossHair.transform.position = Input.mousePosition;

        //for jumping
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded && justJumped==false)
            {
                if(isCrouched)
                {
                    isCrouched=false;
                }
                else{
                    playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump");
                lastJumpTime=Time.time;
                isGrounded=false;
                justJumped=true;
                }
            }
        }

        //for crouching 
        if(Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            if(isCrouched==false)
            {
                isCrouched=true;
                Crouch();
            }
            else
            {
                isCrouched=false;
            }
        }

        if(isCrouched)
        {
            playerAnim.SetBool("Crouched", true);
        }
        else
        {
            playerAnim.SetBool("Crouched", false);
        }

        //for attacking
        if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            attacking=true;
        }
        else
        {
            attacking=false;
        }
		
		
		//Raycasts
		
        
    }
	
	

    void CalculateAnimations(float horizontalInput, float verticalInput)
    {
        if(horizontalInput==0 && verticalInput==0)
        {
            playerAnim.SetBool("Idle", true);
        }
        else 
        {
            playerAnim.SetBool("Idle",false);
        }

        if(isSprinting)
        {
            playerAnim.SetBool("Sprint",true);
        }
        else 
        {
            playerAnim.SetBool("Sprint",false);
        }

        if(horizontalInput==0)
        {
            playerAnim.SetBool("HorizontalZero",true);
        }
        else
        {
            playerAnim.SetBool("HorizontalZero",false);
        }

        if(verticalInput==0)
        {
            playerAnim.SetBool("VerticalZero",true);
        }
        else
        {
            playerAnim.SetBool("VerticalZero", false);
        }

       if(isGrounded)
       {
           playerAnim.SetBool("InAir", false);
       }
       else
       {
           playerAnim.SetBool("InAir", true);
       }

       if(Input.GetMouseButton(0))
       {
           playerAnim.SetBool("Attack", true);
       }
       else 
       {
           playerAnim.SetBool("Attack", false);
       }
       


        playerAnim.SetFloat("Horizontal", horizontalInput);
        playerAnim.SetFloat("Vertical",verticalInput);

    
    }

    void Rotations()
    {
        turn.x+=Input.GetAxis("Mouse X") * xSense;
		transform.localRotation= Quaternion.Euler(0, turn.x, 0);
    }

    void CalculateMovements(float horizontalInput, float verticalInput)
    {
        	
		Vector3 mousePos= Input.mousePosition;
		
		currSpeed=0.0f;
		
		//checks for front or back movement
		if(verticalInput<0)
		{
			currSpeed = walkSpeed;
		}
		else if(verticalInput>0||horizontalInput!=0)
		{
			currSpeed=frontSpeed;
		}
		
		//gets sprint bool
		if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift))
		{
			isSprinting=true;
		}
		else
		{
			isSprinting=false;
			
			if(verticalInput>0||horizontalInput!=0)
			{
				currSpeed=walkSpeed;
			}
		}
		
	

        if(isCrouched)
        {
            currSpeed=walkSpeed;
        }

		//moves 

        if(collidingEnemy == false)
        {
            	transform.Translate(Vector3.forward * currSpeed * verticalInput * Time.deltaTime);
		    transform.Translate(Vector3.right * currSpeed * horizontalInput * Time.deltaTime);
        }
        else
        {
            if(!isGrounded)
            {
                transform.Translate(Vector3.forward * currSpeed * verticalInput * Time.deltaTime);
		         transform.Translate(Vector3.right * currSpeed * horizontalInput * Time.deltaTime);
            }
        }
	
    }


     void OnCollisionEnter(Collision other)
    {
       
	
		
		 if(other.gameObject.CompareTag("Ground"))
        {
           
                isGrounded=true;
            

            if(falling)
            {
                playerAudio.Stop();
                falling=false;
            }

            if(fellOffMap)
            {
                GetComponent<PlayerGame>().DrainHealth(50f);
                fellOffMap = false;
            }
        }
       
	
    }


  


      public void MouseLock()
    {
        Cursor.lockState=CursorLockMode.Locked;
    }

    private void Fall()
    {
        if(isGrounded==false && falling==false)
        {
            playerAudio.clip=fallingSound;
            playerAudio.Play();
            falling=true;
        }
    }

    private void Step(int anim)
    {
        if(stepCount==0)
        {
            lastAnim=anim;
        }

        if(anim==lastAnim)
        {
            playerAudio.PlayOneShot(stepSound);

            lastAnim=anim;
        }
        else
        {
            if(playerAudio.isPlaying==false)
            {
                playerAudio.PlayOneShot(stepSound);
                
                lastAnim=anim;
            }
        }

        stepCount++;
    }

    private void Attack()
    {
        playerAudio.PlayOneShot(attackSound, 0.15f);
    }

    private void Crouch()
    {
        playerAudio.PlayOneShot(crouchSound, 0.15f);
    }

    private void Jump()
    {
        playerAudio.PlayOneShot(jumpSound, 0.15f);
    }


    public void UpdatePosition()
    {
        worldLoad.playerXPos = transform.position.x;
        worldLoad.playerYPos = transform.position.y;
        worldLoad.playerZPos = transform.position.z;
        
    }


    public void Hit()
    {
        Debug.Log("The player has been hit by a zombie");
    }

   void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            collidingEnemy = true;
        }
    }


    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            collidingEnemy = false;
        }
    }

   


  
}
