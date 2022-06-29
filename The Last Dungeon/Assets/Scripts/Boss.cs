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
    public float health;
   // conta o tempo para os ataques
    private float _time;

    
    public enum States
    {
        Walk,
        Attack,
       
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
        if(health == 0)
        {
            state = States.Death;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health -= 50;
            Debug.Log(health);
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
         if(other.CompareTag("Sword"))
        {
               TakeDamage(20);
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
        anim.SetBool("isWalking", false);
    }
    void Walk()
    {
        anim.SetBool("isWalking", true);
        move = Vector3.forward * 1f;
        Vector3 l1 = player.transform.position - transform.position;
        Vector3 dirwoy=new Vector3(l1.x,0,l1.z);
        transform.forward=dirwoy;

        //conversao de direcao local pra global 
        Vector3 globalmove = transform.TransformDirection(move);
        chtr.SimpleMove(globalmove * 1);
      

        if (Vector3.Distance(transform.position, player.transform.position) < 10)
        {
            state = States.Attack;
        }
      
    }
    void Attack()
    {
        anim.SetBool("isWalking", false);
        if(health > 50)
        {
            anim.SetTrigger("FrontAttack");
        }
        else if(health <=50)
        {
            anim.SetTrigger("SideAttack");
        }
        if (Vector3.Distance(transform.position, player.transform.position) > 10)
        {
            state = States.Walk;
            anim.SetBool("isWalking", true);
        }
    }
    void Death()
    {
        Destroy(gameObject);
    }
    void Damage()
    {
      
    }
    void TakeDamage(int damage)
    {
       health -= damage;
    }
   
}
