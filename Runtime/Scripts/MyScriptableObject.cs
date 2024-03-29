using UnityEngine;
using System.Collections.Generic;
using System;

///<summary>
/// Template for all scriptable objects that need to have a unique ID
///</summary>

[Serializable]
public abstract class ScriptableObject_ID : ScriptableObject
{
    private static HashSet<string> s_usedIDs = new();
    
    [SerializeField]
    private string _uniqueID;

    public string ID => _uniqueID;

    public ScriptableObject_ID()
    {
        if (string.IsNullOrEmpty(_uniqueID))
            _uniqueID = GenerateUniqueID();
    }

    private string GenerateUniqueID()
    {
        string newID = Guid.NewGuid().ToString();

        while (s_usedIDs.Contains(newID))
        {
            newID = Guid.NewGuid().ToString();
        }

        s_usedIDs.Add(newID);
        return newID;
    }
}

/// <summary>
/// The base class for all scriptable object that point to a entity
/// </summary>
[Serializable]
public abstract class EntityScirptableObject : ScriptableObject_ID
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