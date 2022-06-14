using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public static  GameObject player;
    Animator anim;
   //Caracteristicas do Boss
    public CharacterController chtr;
    Vector3 move;
    float myrot;
    public float Health;
   // conta o tempo para os ataques
    private float _time;
    
    public enum States
    {
        Walk,
        Attack,
        RockAttack,
        Death,
        TakeDamage,
        Idle
    }
    public States state = States.Idle;
    void Start()
    {
        if (!chtr)
            chtr = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
        Health = 1000;

    }
    private void Update()
    {
        switch (state)
        {
            case States.Walk:
                Walk();
                break;
            case States.Attack:
                Attack();
                break;
            case States.RockAttack:
                RockAtacck();
                break;
            case States.Death:
                Death();
                break;
            case States.TakeDamage:
                Damage();
                break;
            case States.Idle:
                Idle();
                break;
        }
        if(Health == 0)
        {
            state = States.Death;
        }
        
    }
   /* private void DoTimer(float countTime = 20f)
    {
        _time = Time.deltaTime;
        if(_time >= countTime)
        {
           Debug.Log("deu Tempo");
            _time = 0;
        }
    }*/
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = States.Walk;
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = States.Idle;
        }
    }
    void Idle()
    {
        anim.SetBool("MOVING", false);
    }
    void Walk()
    {
        anim.SetBool("MOVING", true);
        move = Vector3.forward * 1f;
        Vector3 l1 = player.transform.position - transform.position;
        Vector3 dirwoy=new Vector3(l1.x,0,l1.z);
        transform.forward=dirwoy;

        //conversao de direcao local pra global 
        Vector3 globalmove = transform.TransformDirection(move);
        chtr.SimpleMove(globalmove * 1);
      

        if (Vector3.Distance(transform.position, player.transform.position) < 3)
        {
            state = States.Attack;
        }
      
    }
    void Attack()
    {
        anim.SetBool("ATACCK", true);
        anim.SetBool("MOVING", false);
        if (Vector3.Distance(transform.position, player.transform.position) > 4)
        {
            state = States.Walk;
            anim.SetBool("ATACCK", false);
            anim.SetBool("MOVING", true);
        }
    }
    void RockAtacck()
    {
        anim.SetBool("ROCKATACCK", true);
        anim.SetBool("MOVING", false);
    }
    void Death()
    {
      
    }
    void Damage()
    {

    }
}
