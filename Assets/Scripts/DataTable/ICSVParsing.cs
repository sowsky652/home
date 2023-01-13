using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public interface ICSVParsing { 

    string id { get; set; }

    void Parser(Dictionary<string, string> line);
}
