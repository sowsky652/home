using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    Weapon,
    Armor,
    Consumable,
}

public class ItemData : ICSVParsing
{
    //each item info
    public string id { get; set ; }
    public ItemTypes type;
    public string name;   //ID
    public string desc;   //ID
    public string iconSpriteId;  //Resources pth

    private Sprite iconSprite;

    public Sprite IconSprite
    {
        get
        {
            if (iconSprite == null)
            {
                iconSprite = Resources.Load<Sprite>(iconSpriteId);
            }

            return iconSprite;
        }
    }


    public void Parser(Dictionary<string, string> line)
    {
        id = line["ID"];
        type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), line["Type"]);
        name = line["Name"];
        desc = line["Desc"];
        iconSpriteId = line["IconSpriteId"];

    }

}
