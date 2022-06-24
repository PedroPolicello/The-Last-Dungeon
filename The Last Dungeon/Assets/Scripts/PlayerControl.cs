using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

  public bool _isOnGround; 
  private bool _canJump;
  private bool _isStuned;

  private Vector3 _move;
  private Vector3 _rotate; 
  private float _forcemove=1000;
  private float _drag=4;
  
  public enum States
  {
     Atacck,
     Jump,
     Collect,
     Damage,
     Magic,
     Dead,
     Idle,
     Stuned,
     Walk
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
      else 
      {
         _move = Vector3.zero;
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
          state = States.Idle;
          anim.SetFloat("jump" , 0);
       }
      if(_canJump && Input.GetKeyDown(KeyCode.Space))
      {
         rgb.AddForce(0,2500,0);
         anim.SetFloat("jump" , 1);
      }
      

   }
 void FixedUpdate()
    {
        float vel = rgb.velocity.magnitude;
        rgb.AddForce((_move * _forcemove)/ (vel*2+1));
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
       // else if(!_isStuned)
       // {
       //    _isOnGround = true;
       // }
        
    }
   
   void Idle()
   {
      
      
       if (_move.magnitude > 0)
        {
           state = States.Walk;
          
        }
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
         if(_move.magnitude < 0)
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
