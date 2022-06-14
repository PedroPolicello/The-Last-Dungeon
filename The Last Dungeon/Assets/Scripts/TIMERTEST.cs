using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIMERTEST : MonoBehaviour
{ 
    private float _time;
   
    void Update()
    {
        DoTimer();
    }
    private void DoTimer(float countTime = 5f)
    {
        _time += Time.deltaTime;
        if(_time >= countTime)
        {
           Debug.Log("deu Tempo");
            _time = 0;
        }
    }
}
