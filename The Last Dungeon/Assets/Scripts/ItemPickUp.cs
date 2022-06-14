using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    public static GameObject buttonPickUp;
    private bool _isShield;
    private bool _isStaff;
    private bool _isArco;
    private bool _isSword;

    [Header("Slots do Inventario")]

    public static GameObject shieldSlot;
    public static GameObject swordSlot;
    public static GameObject staffSlot;
    public static GameObject bowSlot;
   

    // Update is called once per frame
    void Update()
    {

        if (buttonPickUp.activeSelf && Input.GetKey(KeyCode.E) && _isShield )
        {
           
            buttonPickUp.SetActive(false);
            ItemEquip.shieldOn = true;
            shieldSlot.SetActive(true);
        }
        else if (buttonPickUp.activeSelf && Input.GetKey(KeyCode.E) && _isStaff)
        {
            Debug.Log("deu");
            staffSlot.SetActive(true);
            buttonPickUp.SetActive(false);
            ItemEquip.staffOn = true;
        }
        else if (buttonPickUp.activeSelf && Input.GetKey(KeyCode.E) && _isArco)
        {
            bowSlot.SetActive(true);
            buttonPickUp.SetActive(false);
            ItemEquip.bowOn = true;
        }
        else if (buttonPickUp.activeSelf && Input.GetKey(KeyCode.E) && _isSword)
        {
            swordSlot.SetActive(true);
            buttonPickUp.SetActive(false);
            ItemEquip.swordOn = true;
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            buttonPickUp.SetActive(true);
            _isShield = true;
            _isStaff = false;
             _isArco = false;
             _isSword = false;
        }
        else if(other.CompareTag("staff"))
        {
            buttonPickUp.SetActive(true);
            _isStaff = true;
            _isShield = false;
            _isArco = false;
            _isSword = false;
        }
        else if(other.CompareTag("Arco"))
        {
            buttonPickUp.SetActive(true);
            _isStaff = false;
            _isShield = false;
            _isArco = true;
            _isSword = false;
        }
        else if(other.CompareTag("Sword"))
        {
            buttonPickUp.SetActive(true);
            _isStaff = false;
            _isShield = false;
            _isArco = false;
            _isSword = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            buttonPickUp.SetActive(false);
        }
    }
}
