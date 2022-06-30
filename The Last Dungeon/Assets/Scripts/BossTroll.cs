using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTroll : MonoBehaviour
{
    public static GameObject player;
    Animator anim;
   [Header("characterController do boss")]
    public CharacterController chtr;
    Vector3 move;
    float myrot;
    public float health;
    // conta o tempo para os ataques
    private float _time;
    public static bool isInRange;

    [Header("Area de ataque do pisão")]
    public GameObject footAttack;


    public enum States
    {
        Walk,
        Attack,
        Death,
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
            case States.Idle:
                Idle();
                break;
        }
        if (health == 0)
        {
            state = States.Death;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health -= 50;
            Debug.Log(health);
        }
        if(isInRange)
        {
            state = States.Walk;
            if (Vector3.Distance(transform.position, player.transform.position) < 7)
            {
                state = States.Attack;
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("swordAttack"))
        {
            TakeDamage(20);
            anim.SetTrigger("isHeadshot");
            Debug.Log(health);
        }

    }
    void Idle()
    {
        anim.SetBool("isWalking", false);
    }
    void Walk()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 5)
        {
            footAttack.SetActive(false);
        }
        anim.SetBool("isWalking", true);
        move = Vector3.forward * 1f;
        Vector3 l1 = player.transform.position - transform.position;
        Vector3 dirwoy = new Vector3(l1.x, 0, l1.z);
        transform.forward = dirwoy;

        //conversao de direcao local pra global 
        Vector3 globalmove = transform.TransformDirection(move);
        chtr.SimpleMove(globalmove * 1);


        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            state = States.Attack;
          
        }

    }
    void Attack()
    {
        DoTimer();
        anim.SetBool("isWalking", false);
       // if (health > 50)
       // {
            anim.SetTrigger("isAttacking");
       // }
       // else if (health <= 50)
       // {
       //     anim.SetTrigger("SideAttack");
       // }
        if (Vector3.Distance(transform.position, player.transform.position) > 5)
        {
            state = States.Walk;
            anim.SetBool("isWalking", true);
        }
    }
    void Death()
    {
        anim.SetBool("isDying", true);
    }
    void TakeDamage(int damage)
    {
        health -= damage;
    }
    private void DoTimer(float time = 2f)
    {
        _time += Time.deltaTime;
        if (_time >= time)
        {
            Debug.Log("deu Tempo");
            _time = 0;
            footAttack.SetActive(true);
        }
       // else
       // {
       //     footAttack.SetActive(false);
       // }
    }
}
