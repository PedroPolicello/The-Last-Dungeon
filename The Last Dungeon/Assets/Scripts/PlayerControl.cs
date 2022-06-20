using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
  public HealthControl healthbar;
  public GameObject dummyCam;

  Rigidbody rgb;
  private int _maxhealth = 100;
  private int _currentHealth;
  public bool _isOnGround; 
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
     Idle
  } 
   public States state = States.Idle;
   public void Start()
   {
      _currentHealth = _maxhealth;
      healthbar.SetMaxHealth(_maxhealth);
      rgb = GetComponent<Rigidbody>();
   }
  
         void Update(){

             _move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (dummyCam) 
            _move = dummyCam.transform.TransformDirection(_move);
       
        _move = new Vector3(_move.x,0,_move.z);
        if (_move.magnitude > 0 && _isOnGround )
        {
            transform.forward = Vector3.Slerp(transform.forward,_move,Time.deltaTime*10);
        }
         }
 void FixedUpdate()
    {
        float vel = rgb.velocity.magnitude;
        rgb.AddForce((_move * _forcemove)/ (vel*2+1));
        Vector3 velwoy = new Vector3(rgb.velocity.x, 0, rgb.velocity.z);
        rgb.AddForce(-velwoy * _drag);

        switch (state)
       {
           case States.Idle:
              Idle();
              break;
       }
       if(Input.GetKeyDown(KeyCode.E))
        {
          TakeDamage(20);
        }
        
    }
   
   void Idle()
   {

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
       }
   }
}
