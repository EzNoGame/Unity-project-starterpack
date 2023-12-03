using UnityEngine;
using System.Collections.Generic;
using System;

///<summary>
/// Template for all scriptable objects that need to have a unique ID
///</summary>

[Serializable]
public abstract class ScriptableObject_ID : ScriptableObject
{
    private static HashSet<string> usedIDs = new();

    [SerializeField]
    private string uniqueID;

    public string ID => uniqueID;

    public ScriptableObject_ID()
    {
        if (string.IsNullOrEmpty(uniqueID))
            uniqueID = GenerateUniqueID();
    }

    private string GenerateUniqueID()
    {
        string newID = Guid.NewGuid().ToString();

        while (usedIDs.Contains(newID))
        {
            newID = Guid.NewGuid().ToString();
        }

        usedIDs.Add(newID);
        return newID;
    }
}

/// <summary>
/// The base class for all scriptable object that point to a entity
/// </summary>
[Serializable]
public abstract class MyScriptableObject : ScriptableObject_ID
{
    [SerializeField]
    protected Sprite _icon;
    [SerializeField]
    protected string _description;
    [SerializeField]
    protected GameObject _behaviourPrefab;

    public void Initialize(Sprite icon, string description, GameObject behaviour)
    {
        _icon = icon;
        _description = description;
        _behaviourPrefab = behaviour;
    }

    public string Name => name;
    public Sprite Icon => _icon;
    public string Description => _description;
    public GameObject Behaviour => _behaviourPrefab;
}