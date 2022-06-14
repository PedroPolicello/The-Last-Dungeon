using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TrdControl : MonoBehaviour
    
{

    public HealthControl healthbar;
    public int Maxhealth = 100;
    public int CurrentHealth;
    // Start is called before the first frame update
    Rigidbody rdb;
    public Vector3 move, rot;
    Animator anim;
    public float forcemove=1000;
    public GameObject dummyCam;
    public float drag=4;
    public AudioSource scream;
    public float jumpForce =500;
    float timeJump = 1;
    float ikforce = 0;
    public bool grab = false;
    FixedJoint grabjoint;
    public GameObject projetil;
    public int JumpTimes;
     bool CanJump =true;
    public enum States
    {
        Walk,
        Attack,
        Idle,
        Dead,
        Damage,
        Jump,
        Spell,
    }

    public States state;

    void Start()
    {
        Cursor.visible = false;
       
        CurrentHealth = Maxhealth;
        healthbar.SetMaxHealth(Maxhealth);
        rdb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        /*

        Joint[] joints;
        joints = GetComponentsInChildren<Joint>();
        foreach (Joint myjoint in joints)
        {
            Destroy(myjoint);
        }

        Rigidbody[] rdbs;
        rdbs = GetComponentsInChildren<Rigidbody>();
        for(int i=1;i<rdbs.Length;i++)
        {
            Destroy(rdbs[i]);
        }
        */
        rdb.isKinematic=false;
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            if (CommomValues.ShrinePlayerPosition.magnitude > 0)
            {
                transform.position = CommomValues.ShrinePlayerPosition;
            }
        }
        
        StartCoroutine(Idle());
        
    }

    public void SetDummyCam(GameObject dummy)
    {
        dummyCam = dummy;
    }

    void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        healthbar.SetHealth(CurrentHealth);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            AiRanged.life = 0;
        }
        if (CurrentHealth <= 0)
        {
            SceneManager.LoadScene("YouDied");
        }

        //criacao de vetor de movimento local
       move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
     
         if (dummyCam) 
         move = dummyCam.transform.TransformDirection(move);

         move = new Vector3(move.x,0,move.z);
      
        if (move.magnitude > 0 && !grab)
        {
            transform.forward = Vector3.Slerp(transform.forward,move,Time.deltaTime*10);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeState(States.Attack);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeState(States.Spell);
        }
        if (Input.GetButtonDown("Jump")&& CanJump == true)
        {
            ChangeState(States.Jump);
            
        }
        if (Input.GetButtonUp("Jump"))
        {
           if(timeJump > 0.1f)
            {
                timeJump = 0.05f;
            }
        }
        grab = Input.GetButton("Fire3");
    }


    private void FixedUpdate()
    {

        float vel = rdb.velocity.magnitude;

        //limite de velocidade
        rdb.AddForce((move * forcemove)/ (vel*2+1));
        anim.SetFloat("Velocity", vel);

        //velocidade sem y
        Vector3 velwoy = new Vector3(rdb.velocity.x, 0, rdb.velocity.z);
        //drag manual
        rdb.AddForce(-velwoy * drag);

        if(Physics.Raycast(transform.position+Vector3.up*.5f,Vector3.down,out RaycastHit hit, 511))
        {
            anim.SetFloat("DistGround", hit.distance);
            if(hit.distance < 0.5f)
            {
                CanJump = true;
            }
            Debug.DrawLine(transform.position + Vector3.up*.5f, hit.point,Color.red);
        }
        if (!grab)
        {
            ikforce = Mathf.Lerp(ikforce, 0, Time.fixedDeltaTime*3);
        }
    }

    public void ChangeState(States myestate)
    {
        state = myestate;
        StartCoroutine(state.ToString());
    }
    public void Grab()
    {
        if (grab)
        {
            if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out RaycastHit hit, 2, 511))
            {
                if (hit.collider.CompareTag("Push"))
                {
                    ikforce = Mathf.Lerp(ikforce, 1, Time.fixedDeltaTime * 3);
                }
                if (ikforce > 0.9f)
                {
                    if (!grabjoint)
                    {
                        grabjoint = hit.collider.gameObject.AddComponent<FixedJoint>();
                        grabjoint.connectedBody = rdb;
                    }
                }   
            }
        }
        else { 
            if (grabjoint)
            {
                Destroy(grabjoint);
            }
        }
    }

    IEnumerator Idle()
    {
        //entrada
        while (state == States.Idle)
        {
            //loop
            yield return new WaitForFixedUpdate();

            if (rdb.velocity.magnitude > 0.1f)
            {
                ChangeState(States.Walk);
            }
            Grab();
        }
        //saida
    }

    IEnumerator Walk()
    {
        //entrada
        
        while (state == States.Walk)
        {
            //loop
            yield return new WaitForFixedUpdate();
            if (rdb.velocity.magnitude < 0.1f)
            {
                ChangeState(States.Idle);
            }
            Grab();

        }
        //saida
    }

    IEnumerator Attack()
    {
        //entrada
        anim.SetTrigger("Atack");
        scream.Play();
        yield return new WaitForSeconds(2);
        
        ChangeState(States.Idle);
        //saida
    }

    IEnumerator Spell()
    {
        //entrada
        anim.SetTrigger("Spell");
        scream.Play();
        yield return new WaitForSeconds(0.7f);
        GameObject magic = Instantiate(projetil, transform.position + Vector3.up+transform.forward, transform.rotation);
        magic.GetComponent<Rigidbody>().AddForce(transform.forward * 50, ForceMode.Impulse);

        yield return new WaitForSeconds(1);
       

        ChangeState(States.Idle);
        //saida
    }

    IEnumerator Damage()
    {
        //entrada
        while (state == States.Damage)
        {
            //loop
            yield return new WaitForFixedUpdate();
        }
        //saida
    }

    IEnumerator Dead()
    {
        //entrada
        while (state == States.Dead)
        {
            //loop
            yield return new WaitForFixedUpdate();
        }
        //saida
    }
    IEnumerator Jump()
    {
        timeJump = .4f;
        CanJump = false;   
        //entrada
         while (state == States.Jump)
         {
             yield return new WaitForFixedUpdate();
             //loop
        
             rdb.AddForce(Vector3.up * jumpForce * timeJump);
             timeJump -= Time.fixedDeltaTime;
             if (timeJump <= 0)
             {
                 ChangeState(States.Idle);
                 
             }
         }
        //saida
    }

    void OnAnimatorIK(int layerIndex)
    {
        

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikforce);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikforce);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, transform.position + Vector3.up + transform.forward - transform.right * 0.5f);
        anim.SetIKPosition(AvatarIKGoal.RightHand, transform.position + Vector3.up + transform.forward + transform.right * 0.5f);

        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, ikforce);
        anim.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Euler(-40, 131, 301));//124 307 301

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Flecha"))
        {
            TakeDamage(5);
        }
         if(other.CompareTag("ROOM1"))
        {
            
            SpawEnimes.live = true;
           
            Debug.Log("true");
        }
    }
   
}
