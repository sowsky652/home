using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    private ItemData data;
    public Image icon;
    public Button button; 

    public ItemData Data
    {
        get => data;
        set { data = value; }
    }
    public void SetEmpty()
    {
        button.interactable= false;
        data = null;
        icon.sprite = null; 
    }
    public void Set(ItemData data)
    {
        this.data = data;
        icon.sprite = data.IconSprite;
        button.interactable = true;

    }
}
