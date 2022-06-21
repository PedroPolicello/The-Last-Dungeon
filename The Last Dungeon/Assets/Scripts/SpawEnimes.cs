using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawEnimes : MonoBehaviour
{

    public static bool live;
    public static float control = 0;
    public GameObject Pota;
    public bool open;
    // ------ Local de Spawn dos arqueiros -------
    public GameObject Spawn1, Spawn2, Spawn3, Spawn4;
    
    // ------ prefeb dos inimigos-----
    public GameObject Enemy1;

    // ----- paredes --------
    public GameObject wall1, wall2;

   
    void Update()
    {
      if(open)//(live == true && control == 0)
        { 
            GameObject inimigo1 = Instantiate(Enemy1, Spawn1.transform.position,  Quaternion.Euler(0,0,0));
            GameObject inimigo2 = Instantiate(Enemy1, Spawn2.transform.position,  Quaternion.Euler(0,0,0));
            GameObject inimigo3 = Instantiate(Enemy1, Spawn3.transform.position,  Quaternion.Euler(0,0,0));
            GameObject inimigo4 = Instantiate(Enemy1, Spawn4.transform.position,  Quaternion.Euler(0,0,0));
            live = false;
            Pota.SetActive(false);
            wall1.SetActive(true);
            wall2.SetActive(true);
            Debug.Log("abriu");
        }
       if( control >= 5)
       {
        control = 0;
        Debug.Log(control);
        wall1.SetActive(false);
        wall2.SetActive(false);
       }
        
    }
    void OnTriggerEnter(Collider other)
    {
      if(other.CompareTag("Player"))
      {
        open = true;

      }
    }
}
