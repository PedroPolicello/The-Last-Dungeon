using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblin : MonoBehaviour
{
    public static GameObject player;
    Animator anim;
    //Caracteristicas do Boss
    public CharacterController chtr;
    Vector3 move;
    float myrot;
    public float health;
    void Start()
    {
        if (!chtr)
            chtr = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        move = Vector3.forward * 2f;
        //Vector3 l1 = player.transform.position - transform.position;
        //Vector3 dirwoy = new Vector3(l1.x, 0, l1.z);
        //transform.forward = dirwoy;
        anim.SetBool("isRun", true);

        //conversao de direcao local pra global 
        Vector3 globalmove = transform.TransformDirection(move);
        chtr.SimpleMove(globalmove * 1);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Sword"))
        {
            health -= 20;
            anim.SetTrigger("isHit");
            anim.SetBool("isRun", false);
            Debug.Log("hit");
        }
    }
}
