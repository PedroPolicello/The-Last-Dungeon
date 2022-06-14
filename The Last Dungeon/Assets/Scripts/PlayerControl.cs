using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
  [Header("Player Health")]
  public HealthControl healthbar;
  
  public GameObject dummyCam;
 
  Rigidbody rgb;
  private int _maxhealth = 100;
  private int _currentHealth;
  private bool _isOnGround; 
  private Vector3 _move;
  private Vector3 _rotate; 
  
  public enum States
  {
     Walk,
     ATACCK,
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
   }
   public void Update()
   {
       switch (state)
       {
           case States.Walk:
              Walk();
              break;
           case States.Idle:
              Idle();
              break;
       }
       if(Input.GetKeyDown(KeyCode.E))
        {
          TakeDamage(20);
        }
         _move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
         if (dummyCam) 
         state = States.Walk;
         

   }
   void Walk()
   {
      _move = dummyCam.transform.TransformDirection(_move);
       _move = new Vector3(_move.x,0,_move.z);
        if (_move.magnitude > 0) //&& !_isOnGround )
        {
            transform.forward = Vector3.Slerp(transform.forward,_move,Time.deltaTime*10);
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
