using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : MonoBehaviour
{
    public GameObject door;

    public static GameObject player;
    Animator anim;
    //Caracteristicas do Boss
    public CharacterController chtr;
    Vector3 move;
    float myrot;
    public float health;
    // conta o tempo para os ataques
    private float _time;
    public static bool isInRange;
    public static bool _isStuned;

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
            case States.Idle:
                Idle();
                break;
        }
        if (health <= 0)
        {
            door.SetActive(true);
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health -= 50;

        }
        if (isInRange)
        {
            state = States.Walk;
            if (Vector3.Distance(transform.position, player.transform.position) < 10)
            {
                state = States.Attack;
            }
        }
    }
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            TakeDamage(20);
            
        }
        if (other.CompareTag("Player"))
        {
            Vector3 l1 = player.transform.position - transform.position;
            myrot = Mathf.LerpAngle(myrot, Vector3.SignedAngle(transform.forward, l1, Vector3.up), Time.deltaTime * 10);
            transform.Rotate(new Vector3(0, myrot, 0));
        }
        if(other.CompareTag("shieldEquip"))
        {
            _isStuned = true;
        }

    }
    void Idle()
    {
        anim.SetBool("isWalking", false);
    }
    void Walk()
    {
        anim.SetBool("isWalking", true);
        move = Vector3.forward * 5f;
        Vector3 l1 = player.transform.position - transform.position;
        Vector3 dirwoy = new Vector3(l1.x, 0, l1.z);
        transform.forward = dirwoy;

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
        if (health > 50)
        {
            anim.SetTrigger("FrontAttack");
        }
        else if (health < 50)
        {
            anim.SetTrigger("SideAttack");
        }
        if (Vector3.Distance(transform.position, player.transform.position) > 13)
        {
            state = States.Walk;
            anim.SetBool("isWalking", true);
        }
    }
    void Death()
    {
        Destroy(gameObject);
        Debug.Log("morreu");
    }
    void TakeDamage(int damage)
    {
        health -= damage;
    }
    void Dotimer(float time = 3f)
    {
        _time += Time.deltaTime;
        if (_time >= time)
        {
            Debug.Log("deu Tempo");
            _time = 0;
            _isStuned = false;
        }
    }

}
