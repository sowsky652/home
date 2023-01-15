using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiItemInfo : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDesc;

    private void Awake()
    {
        SetEmpty();
    }

    public void SetEmpty()
    {
        image.sprite = null;
        itemName.text = string.Empty;
        itemDesc.text = string.Empty;
    }
    public void Set(ItemData data)
    {
        if (data == null)
        {

            SetEmpty();
            return;
        }
        itemName.text = data.name;
        itemDesc.text = data.desc;
        if (data.IconSprite != null)
            Debug.Log($"{data.IconSprite}");
            //image.sprite = data.IconSprite;

    }
}
