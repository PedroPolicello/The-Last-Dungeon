using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarFlechas : MonoBehaviour
{
    public Transform spawnFlecha;
    public GameObject flecha;
    public bool atirando = true;
    public float x;
    public float y;
    public float z;
    
    


    public void Start()
    {
        InvokeRepeating("Flecha", 2, 2);
    }

    public void Flecha()
    {
       
       GameObject clone = Instantiate(flecha, new Vector3(x, y, z), Quaternion.Euler(0,90,0));
        Destroy(clone, 0.6f);
        clone.GetComponent<Rigidbody>().AddForce(-transform.right * 2700);

    }
}
