using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flecha : MonoBehaviour
{
    // Start is called before the first frame update
   void OnTriggerEnter(Collider other)
   {
       if(other.CompareTag("Player"))
       {
           Destroy(gameObject);
       }
       if(other.CompareTag("shieldEquip"))
        {
            Destroy(gameObject);
        }
   }
}
