using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiRanged : MonoBehaviour
{
    public CharacterController chtr;
    Vector3 move;
    public GameObject player;
    float myrot;
    public bool atacck;
    // --- vida do inimigo ---
    public static float life = 100;
    // --- local de ataque ---
    public GameObject spawnFlecha;
    public GameObject flecha;
    public Vector3 pointshot;
    public bool atirando = true;
    public float x;
    public float y;
    public float z;
    float delay =0;
    
    public enum States
    {
        Walk,
        Attack,
        Dead,
        Damage,
        Shot
       
    }

    public States state = States.Walk;

    void Start()
    {
        if (!chtr)
            chtr = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
       
    }


    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
               case States.Walk:
               walk();
                  break;
               case States.Attack:
                     attack();
                  break;
               case States.Dead:
                     Dead();
                   break;
                case States.Damage:
                   break;
                case States.Shot:
                   shot();
                break;
                 
        }
      
        if(life == 0)
        {
            state = States.Dead;
        }
       

        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           state = States.Attack;
           Debug.Log("atirar");
        }
        
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          state = States.Walk;
        }
    }
    void Dead()
    {
      Destroy(gameObject);
      SpawEnimes.control += 1;
      Debug.Log(SpawEnimes.control);
    }
    void walk()
    {
        
    }

    void attack()
    {

        move = Vector3.forward * 1f;
            Vector3 l1 = player.transform.position - transform.position;
        Vector3 dirwoy=new Vector3(l1.x,0,l1.z);
        transform.forward=dirwoy;



        //conversao de direcao local pra global 
        Vector3 globalmove = transform.TransformDirection(move);
        chtr.SimpleMove(globalmove * 1);
            


    if (Vector3.Distance(transform.position, player.transform.position) < 10)
        {
              state = States.Shot;
        }
       
        
    }

    void shot()
    {
        Vector3 l1 = player.transform.position - transform.position;
        myrot = Mathf.LerpAngle(myrot, Vector3.SignedAngle(transform.forward, l1, Vector3.up), Time.deltaTime * 10);
        transform.Rotate(new Vector3(0, myrot, 0));

        if (Vector3.Distance(transform.position, player.transform.position) < 10)
        {
           Flecha(Time.time);
        }

    }

    void Flecha(float tempo){
            if(tempo>delay+0.7f){

            
            pointshot = spawnFlecha.transform.position;
            GameObject clone = Instantiate(flecha, spawnFlecha.transform.position, gameObject.transform.rotation);
            clone.transform.Rotate(0,gameObject.transform.rotation.y + 90,0);

            
            //clone.transform.localRotation = Quaternion.identity;
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            Destroy(clone, 0.6f);
            delay=tempo;
            }

    }
   

}
