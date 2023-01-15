using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ONOFF : MonoBehaviour
{
    GameObject inven;
    GameObject info;
    // Start is called before the first frame update
    void Start()
    {
        inven = GameObject.FindGameObjectWithTag("Inventory");
        info = GameObject.FindGameObjectWithTag("Stats");

    }

    public void ToggleInven()
    {
        inven.SetActive(!inven.active); 
    }

    public void ToggleInfo()
    {
        info.SetActive(!info.active);
    }


}
