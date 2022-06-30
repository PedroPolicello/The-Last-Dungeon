using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
  public HealthControl healthbar;
  public GameObject dummyCam;

  Animator anim;

  Rigidbody rgb;
  private int _maxhealth = 100;
  private int _currentHealth;
  private float _time;
  private float _jumpForce;
  private int _isDefending;

  public bool _isOnGround; 
  private bool _canJump;
  private bool _isStuned;
  private Vector3 _move;
  private Vector3 _rotate; 
  private float _forcemove=1000;
  private float _drag=4;
  
  public enum States
  {
     Attack,
     Jump,
     Collect,
     Damage,
     Magic,
     Dead,
     Idle,
     Stuned,
     Walk,
     Defend
  } 
   public States state = States.Idle;
   public void Start()
   {
      _currentHealth = _maxhealth;
      healthbar.SetMaxHealth(_maxhealth);
      rgb = GetComponent<Rigidbody>();
      anim = GetComponent<Animator>();
   }
   void Update()
   {
      if(_isOnGround && !_isStuned)
      {
         _move = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
      }
      if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)) 
      {
        rgb.constraints = RigidbodyConstraints.FreezeRotation;
        rgb.velocity = new Vector3 (0,0,0);
      }
        if (dummyCam) 
        {
            _move = dummyCam.transform.TransformDirection(_move);
        }
         _move = new Vector3(_move.x,0,_move.z); 
        if (_move.magnitude > 0 && _isOnGround)
        {
           transform.forward = Vector3.Slerp(transform.forward,_move,Time.deltaTime*10);
        }
       if(_isOnGround)
       {
          anim.SetFloat("jump" , 0);
       }
      if(_canJump && Input.GetKeyDown(KeyCode.Space))
      {
         rgb.AddForce(0,2500,0);
         anim.SetFloat("jump" , 1);
      }
      if(Input.GetKeyDown(KeyCode.O))
      {
         AiRanged.life = 0;
      }
      if(_currentHealth <= 0)
        {
            SceneManager.LoadScene("YouDied");
            ONLOAD._playerIsDead = true;
            Debug.Log(ONLOAD._playerIsDead);
        }

   }
 void FixedUpdate()
    {
        float vel = rgb.velocity.magnitude;
        rgb.AddForce((_move * _forcemove)/ (vel*0.5f+2));
        Vector3 velwoy = new Vector3(rgb.velocity.x, 0, rgb.velocity.z);
        rgb.AddForce(-velwoy * _drag);
      anim.SetFloat("velocity", vel);

        switch (state)
       {
           case States.Idle:
              Idle();
              break;
           case States.Stuned:
              Stuned();
              break;
           case States.Walk:
              Walk();
              break;
           case States.Attack:
               Attack();
               break;
           case States.Defend:
               Defend();
               break;
       }
       
       if(Input.GetKeyDown(KeyCode.E))
        {
          TakeDamage(20);
        }
        if(_isStuned)
        {
           state = States.Stuned;
           DoTimer();
        }
        if(Input.GetKeyDown(KeyCode.Q) && ItemEquip.swordEquip) 
       {
          state = States.Attack;
       }
        if(Input.GetKeyDown(KeyCode.Q) && ItemEquip.shieldEquip)
       {
          _isDefending +=1;
          Debug.Log(_isDefending);
       }
       if(Input.GetKeyDown(KeyCode.Q) && ItemEquip.shieldEquip && _isDefending == 1)
       {
          state = States.Defend;
         
       }
       if(_isDefending >= 2)
          {
             _isDefending = 0;
          }
    }
   
   void Idle()
   {
      
      _isOnGround = true;
       if (_move.magnitude != 0)
        {
           state = States.Walk;
        }
   }
   void Defend()
   {   _isDefending +=1;
      anim.SetBool("isBlocking", true);
      rgb.constraints = RigidbodyConstraints.FreezeRotation;
      rgb.velocity = new Vector3 (0,0,0);
      if(Input.GetKeyDown(KeyCode.Q))
      {
         state = States.Walk;
         anim.SetBool("isBlocking", false);
         
      }
   }
   void Attack()
   {
     
      anim.SetTrigger("Attack");
      _isOnGround = false;
      state = States.Idle;
   }
   void Stuned()
   {
     if(_isStuned == false)
     {
        state = States.Idle;
     }
   }

   void Walk()
   {
     
      if(_isOnGround)
      {
         _move = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
      }
         if(_move.magnitude <= 0)
        {
           state = States.Idle;
        }
       
   }

   void TakeDamage(int damage)
   {
    _currentHealth -= damage; 
    healthbar.SetHealth(_currentHealth);
   }
   void OnTriggerEnter(Collider other)
   {
       if(other.CompareTag("Ground"))
       {
          _isOnGround = true;
          _canJump = true;
       }
       else if(other.CompareTag("Stun"))
       {
           _isStuned = true;
       }
       if(other.CompareTag("flecha"))
       {
          TakeDamage(20);
       }
       if(other.CompareTag("Porta"))
       {
         SpawEnimes._open = true;
       }
       if(other.CompareTag("Head"))
        {
            TakeDamage(20);
            Debug.Log(_currentHealth);
        }
   }
   void OnTriggerExit(Collider other)
   {
        if(other.CompareTag("Ground"))
       {
          _isOnGround = false;
          _canJump = false;
       }
       
   }
    private void DoTimer(float timeStuned = 10f)
    {
        _time += Time.deltaTime;
        if(_time >= timeStuned)
        {
           Debug.Log("deu Tempo");
           _time = 0;
           _isStuned = false;
        }
    }
    
}
