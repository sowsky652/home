using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public int slotCount = 100;
    public UISlot uiSlotPrefab;
    public RectTransform content;
    private List<UISlot> slotList=new List<UISlot>();
    private DataTable<ItemData> itemtable;

    public UiItemInfo itemInfo;

    private void Awake()
    {
        itemtable=DataTableMgr.GetTable<ItemData>(); 
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        for(int i=0;i<slotCount; i++)
        {
            var slot = Instantiate(uiSlotPrefab, content);
            slot.SetEmpty();
            slotList.Add(slot);

            var button = slot.GetComponent<Button>();
            button.onClick.AddListener(() => itemInfo.Set(slot.Data));
        }

        var itemIds = itemtable.GetItemIds();
        for (int i = 0; i < 50; ++i)
        {
            var index = Random.Range(0, itemIds.Count);
            slotList[i].Set(itemtable.Get(itemIds[index]));
        }
    }
}
