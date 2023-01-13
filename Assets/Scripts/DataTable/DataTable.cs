using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataTable
{

}

public class DataTable<T> :DataTable where T : ICSVParsing, new()
{
    private string csvFilePath = string.Empty;
    private Dictionary<string,T> tables= new Dictionary<string,T>();

    public DataTable(string path)
    {
        csvFilePath= path;
        Load();
    }

    public List<string> GetItemIds()
    {
        return tables.Keys.ToList<string>();
    }

    //csv phasing
    public void Load()
    {
        var lines= CSVReader.Read(csvFilePath);
        foreach(var line in lines)
        {
            var data = new T();
            data.Parser(line);
            tables.Add(data.id, data);
        }
    }

    public T Get(string id)
    {
        if (!tables.ContainsKey(id))
            return default;
        return tables[id];
    }
}
