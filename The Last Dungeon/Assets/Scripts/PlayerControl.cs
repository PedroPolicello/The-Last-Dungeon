using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
  [Header("Player Health")]
  public HealthControl healthbar;
  
  private int _maxhealth = 100;
  private int _currentHealth;

  public enum States
  {
     Walk,
     ATACCK,
     Jump,
     Collect,
     Damage,
     Magic,
     Dead
  } 
  
   public void Start()
   {

   }
   
}
