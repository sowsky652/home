using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var itemTable = DataTableMgr.GetTable<ItemData>();
        var ids = itemTable.GetItemIds();

        foreach (var id in ids)
        {

            Debug.Log(itemTable.Get(id).iconSpriteId);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
