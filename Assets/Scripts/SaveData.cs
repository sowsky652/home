using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public abstract class SaveData
{
    public abstract int Version { get; }

    public abstract SaveData VersionUp();
    public abstract SaveData VersionDown();
}

public class SaveDataV1 : SaveData
{
    public override int Version => 1;

    public override SaveData VersionDown()
    {
        throw new System.NotImplementedException();
    }

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}