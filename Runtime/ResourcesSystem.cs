using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ResourcesSystem : Singleton<ResourcesSystem>
{
    private Dictionary<string, ScriptableObject_ID> _gameDataDictionary = new();

    private bool _isLoaded = false;
    public bool IsLoaded => _isLoaded;

    private void OnEnable() => LoadAllResources();

    public void LoadAllResources()
    {
        var Data = Resources.LoadAll<ScriptableObject_ID>("").ToList();
        _gameDataDictionary = Data.ToDictionary(x => x.ID, x => x);
        _isLoaded = true;
    }

    public string GetID(ScriptableObject_ID e) => e.ID;

    public T GetData<T>(string ID) where T : ScriptableObject_ID
    {
        if (_gameDataDictionary.ContainsKey(ID) && _gameDataDictionary[ID] is T)
        {
            return _gameDataDictionary[ID] as T;
        }
        return null;
    }

    public T GetRandomData<T>() where T : ScriptableObject_ID
    {
        var dataList = _gameDataDictionary.Values.OfType<T>().ToList();
        if (dataList.Count > 0)
        {
            return dataList[Random.Range(0, dataList.Count)];
        }
        return null;
    }

    public List<T> GetNonRepeatRandomData<T>(int amount) where T : ScriptableObject_ID
    {
        List<T> dataList = new();
        int i = 1000;
        while(dataList.Count < amount && i > 0)
        {
            i--;
            var data = GetRandomData<T>();
            if (data != null && !dataList.Contains(data))
                dataList.Add(data);
        }
        return dataList;
    }

    public List<T> GetAllData<T>() where T : ScriptableObject_ID
    {
        return _gameDataDictionary.Values.OfType<T>().ToList();
    }
    public T GetDataByName<T>(string Name) where T : ScriptableObject_ID
    {
        var dataList = _gameDataDictionary.Values.OfType<T>().ToList();
        foreach (var data in dataList)
        {
            if (data.name == Name)
            {
                return data;
            }
        }
        return null;
    }
}
