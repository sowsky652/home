using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataTableMgr
{
    private static Dictionary<Type, DataTable> tables = new();
    private static bool isLoaded = false;
    public static void LoadAll()
    {
        tables.Add(typeof(ItemData), new DataTable<ItemData>("Tables/ItemTable"));

        isLoaded = true;
    }

    public static DataTable<T> GetTable<T>() where T : ICSVParsing, new()
    {
        if (!isLoaded)
        {
            LoadAll();
        }
        return tables[typeof(T)] as DataTable<T>;
    }
}
