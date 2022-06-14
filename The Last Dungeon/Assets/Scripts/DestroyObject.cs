using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private bool _isOnRange;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && _isOnRange)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            _isOnRange = true;
        }
    }
   
}
