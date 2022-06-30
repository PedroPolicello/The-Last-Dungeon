using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemys : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Porta"))
        {
            BossTroll.isInRange = true;
            Boss.isInRange = true;
            AiRanged.isInRange = true;
        }
    }
}
