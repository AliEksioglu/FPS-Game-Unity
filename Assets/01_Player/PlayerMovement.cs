using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct FeaturePlayer 
{
    public float maxhP;
    public float damageCurHP { set { 
            _curHP -= value; 
            if(_curHP <= 0)
            {
                GameManager.playerIsDead = true;
                playerIsAlive = false;
            }
        }
        get { return _curHP; }
    }
    public float healtCurHP { set { 
            _curHP += value; 
            if (_curHP > maxhP) _curHP = maxhP; 
        } 
        get { return _curHP; }
    }
    private float _curHP;
    public bool playerIsAlive;

}

public class PlayerMovement : MonoBehaviour , IDamagable  
{
    //GetDamageUI

    [SerializeField] Image getDamagePanel;
    private Color damageColorScrean;

    //--
    //Sound--

    [SerializeField] AudioSource walkSound;    
    [SerializeField] AudioSource runSound;    
    [SerializeField] AudioSource jumpStartSound;    
    [SerializeField] AudioSource jumpEndSound;
    [SerializeField] AudioSource hitDamageSound;

    //---
    
    FeaturePlayer Fplayer = new FeaturePlayer();
    private CharacterController controller;

    public Transform firefighterSPosition;
    [SerializeField] Slider HPbar;
    [SerializeField] private float speed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private GameObject deadEffect;
    [SerializeField] private float mouseSensitivity = 750f;

    private float xRotation = 0f;
    private Transform playerBody;
    private Vector3 moveVelocity;
    private Vector3 moveInputs;

    private float mouseX;
    private float mouseY;
    private float keyboardX;
    private float keyboardY;

    private Vector3 velocity;
    private float gravity = -9.81f;
    static public bool isgorunded;

    [SerializeField] float jumpHeight;
    [SerializeField] float gravityDivide;
    [SerializeField] Transform headT;

    [SerializeField] protected Animator animMove;
    [SerializeField] protected Animator animCamera;

    private bool checkDead = true;
    private void Awake()
    {
        isgorunded = false;
        Fplayer.maxhP = 100;
        Fplayer.healtCurHP = Fplayer.maxhP;
        Fplayer.playerIsAlive = true;
        controller = GetComponent<CharacterController>();
        damageColorScrean = getDamagePanel.color;
        damageColorScrean.a = 0.0f;
        getDamagePanel.color = damageColorScrean;
        playerBody = this.transform;
        
    }
    void Update()
    {
        if (Fplayer.playerIsAlive)
        {
            CharacterMove(); //CharacterMovement
            CharacterRotate(); //CharacterRotate
            JumpAndGravity(); //jumping and gravity
        }
        else if (!Fplayer.playerIsAlive && checkDead)
        {
            PlayerIsDead();
            checkDead = false;
        }

    }
    IEnumerator getdamageUI()
    {
       damageColorScrean.a = 0.6f;
       getDamagePanel.color = damageColorScrean;
       while (damageColorScrean.a > 0.1f)
       {
            damageColorScrean.a -= 0.1f ;
            getDamagePanel.color = damageColorScrean;
            yield return new WaitForSeconds(0.1f);
       }
        damageColorScrean.a = 0f;
        getDamagePanel.color = damageColorScrean;
    }
    public void GetDamagable(AttackDefiniton attack)
    {
        StartCoroutine("getdamageUI");
        hitDamageSound.Play();
        animCamera.Play("giveDamageCamera", 0, 0f);
        Fplayer.damageCurHP = attack.damagePoint;
        HPbar.value -= attack.damagePoint;

    }
    private void CharacterMove()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        keyboardX = Input.GetAxis("Horizontal");
        keyboardY = Input.GetAxis("Vertical");


        //Walk
        if ((keyboardX != 0 || keyboardY != 0) && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        //Runnig
        else if((keyboardX != 0 || keyboardY != 0) && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else if(keyboardX == 0 || keyboardY == 0)
        {
            moveVelocity = new Vector3(0, 0, 0);
            Idle();
        }
        moveInputs = keyboardX * playerBody.right + keyboardY * playerBody.forward;
        moveVelocity = moveInputs * Time.deltaTime * speed;
        controller.Move(moveVelocity);

    }
    private void CharacterRotate()
    {
        if(mouseY != 0 || mouseX != 0)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerBody.Rotate(Vector3.up * mouseX);
            headT.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
    }
    private void JumpAndGravity()
    {
        //isgorunded = Physics.CheckSphere(groundChecker.position, groundCheckerRadius, obstacleLayer);
        if (!isgorunded)
        {
            velocity.y += gravity * Time.deltaTime / gravityDivide;

        }
        else if (isgorunded)
        {
            if (velocity.y < -0.05f)
            {
                jumpEndSound.Play();
            }
            velocity.y = -0.001f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isgorunded)
        {
            walkSound.Stop();
            runSound.Stop();
            jumpStartSound.Play();
            isgorunded = false;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity / gravityDivide * Time.deltaTime);
        }

        controller.Move(velocity);
    }

    //Movement Animation
    private void Idle()
    {
        animMove.SetFloat("Speed",0f, 0.2f, Time.deltaTime);
        walkSound.Stop();
        runSound.Stop();
    }
    private void Walk()
    {
        runSound.Stop();
        if(!walkSound.isPlaying && isgorunded)
        {
            walkSound.Play();
        }
        speed = walkSpeed;
        animMove.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
    }
    private void Run()
    {
        walkSound.Stop();
        if(!runSound.isPlaying && isgorunded)
        {
            runSound.Play();
        }
        speed = runSpeed;
        animMove.SetFloat("Speed", 1f , 0.2f , Time.deltaTime);
    }

    //Player isDead
    private void PlayerIsDead()
    {
        runSound.Stop();
        walkSound.Stop();
        Instantiate(deadEffect, playerBody.position + new Vector3(0,0.25f,1f), Quaternion.identity);
        animMove.enabled = false;
    }
    private void DestroyDroneSound()
    {

    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {

    }

    
}

    
