using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public int slotCount = 100;
    public int haveCount = 50;
    public UISlot uiSlotPrefab;
    public RectTransform content;
    private List<UISlot> slotList = new List<UISlot>();
    private DataTable<ItemData> itemtable;

    public UiItemInfo itemInfo;

    private void Awake()
    {
        itemtable = DataTableMgr.GetTable<ItemData>();
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < slotCount; i++)
        {
            var slot = Instantiate(uiSlotPrefab, content);
            slot.SetEmpty();
            slotList.Add(slot);

            var button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => itemInfo.Set(slot.Data));
        }

        var itemIds = itemtable.GetItemIds();
        for (int i = 0; i < haveCount; ++i)
        {
            var index = Random.Range(0, itemIds.Count);
            slotList[i].Set(itemtable.Get(itemIds[index]));
        }
    }

    public void NameAscending()
    {
        for (int i = 0; i < haveCount-1; i++)
        {
            for (int j = 0; j < haveCount-1; j++)
            {
                if (slotList[i].Data.id.CompareTo(slotList[j+1].Data.id)>0 )
                {
                  
                    var temp = slotList[j].Data;
                    slotList[j].Data = slotList[j + 1].Data;
                    slotList[j + 1].Data = temp;
                }
            }
        }

    }

    public void NameDecending()
    {
        for (int i = 0; i < haveCount-1; i++)
        {
            for (int j = 0; j < haveCount-1; j++)
            {
                if (slotList[i].Data.id.CompareTo(slotList[j + 1].Data.id) < 0)
                {                   

                    var temp = slotList[j].Data;
                    slotList[j].Data = slotList[j + 1].Data;
                    slotList[j + 1].Data = temp;
                }
            }
        }

    }
}
