using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONLOAD : MonoBehaviour
{

    public GameObject PLP;
    public GameObject buttonPickUp;

    [Header("Slots do Inventario")]
    public GameObject shieldSlot;
    public GameObject swordSlot;
    public GameObject staffSlot;
    public GameObject bowSlot;
    private void Awake()
    {
        int Player = FindObjectsOfType<ONLOAD>().Length;
      
        if(Player != 1)
        {
            Destroy(gameObject);
        }
        else
        {
        
            DontDestroyOnLoad(gameObject);
        }
       
        AiRanged.player = PLP;
        ItemPickUp.shieldSlot = shieldSlot;
        ItemPickUp.staffSlot = staffSlot;
        ItemPickUp.swordSlot = swordSlot;
        ItemPickUp.bowSlot = bowSlot;
        ItemPickUp.buttonPickUp = buttonPickUp;

        Boss.player = PLP;
       
    }
}
