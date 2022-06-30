using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquip : MonoBehaviour
{
   
    [Header("Itens do Inventario")]
    public GameObject shield;
    public GameObject staff;
    public GameObject bow;
    public GameObject sword;

    public static bool shieldOn;
    public static bool staffOn;
    public static bool bowOn;
    public static bool swordOn;

    public static bool shieldEquip;
    public static bool swordEquip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && shieldOn)
        {
           
            GameObject lastweapom1 = GameObject.Find("Cajado(Clone)");
            GameObject lastweapom2 = GameObject.Find("Arco (Clone)");
            GameObject lastweapom3 = GameObject.Find("EspadaEquip(Clone)");
            Destroy(lastweapom1);
            Destroy(lastweapom2);
            Destroy(lastweapom3);
            GameObject Myancora = GameObject.Find("Ancora");
            GameObject Myshield = Instantiate(shield, Myancora.transform);
            //Myshield.transform.parent = Myancora.transform;
            Myshield.transform.localPosition = Vector3.zero;
            Myshield.transform.localRotation = Quaternion.identity;
            shieldEquip = true;
            swordEquip = false;

        }
        else if(Input.GetKeyDown(KeyCode.Alpha1) && swordOn)
        {
          GameObject lastweapom1 = GameObject.Find("Escudo(Clone)");
          GameObject lastweapom2 = GameObject.Find("Arco (Clone)");
          GameObject lastweapom3 = GameObject.Find("Cajado(Clone)");
          Destroy(lastweapom1);
          Destroy(lastweapom2);
          Destroy(lastweapom3);
          GameObject Myancora = GameObject.Find("Ancora2");
          GameObject Mysword = Instantiate(sword, Myancora.transform);
          Mysword.transform.localPosition = Vector3.zero;
          swordEquip = true;
          shieldEquip = false;
          
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4) && staffOn)
        {
            GameObject lastweapom1 = GameObject.Find("Escudo(Clone)");
            GameObject lastweapom2 = GameObject.Find("Arco (Clone)");
            GameObject lastweapom3 = GameObject.Find("EspadaEquip(Clone)");
            Destroy(lastweapom1);
            Destroy(lastweapom2);
            Destroy(lastweapom3);
            GameObject Mysword = Instantiate(staff);
            GameObject Myancora2 = GameObject.Find("Ancora2");
            Mysword.transform.parent = Myancora2.transform;
            Mysword.transform.localPosition = Vector3.zero;
            Mysword.transform.localRotation = Quaternion.identity;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && bowOn)
        {
            GameObject lastweapom1 = GameObject.Find("Escudo(Clone)");
            GameObject lastweapom2 = GameObject.Find("Cajado(Clone)");
            GameObject lastweapom3 = GameObject.Find("EspadaEquip(Clone)");
            Destroy(lastweapom1);
            Destroy(lastweapom2);
            Destroy(lastweapom3);
            GameObject Mysword = Instantiate(bow);
            GameObject Myancora2 = GameObject.Find("Ancora2");
            Mysword.transform.parent = Myancora2.transform;
            Mysword.transform.localPosition = Vector3.zero;
            Mysword.transform.localRotation = Quaternion.Euler(45, 180, 90);
           
        }
    }
}
