using UnityEngine;
using UnityEditor;

/// <summary>
/// provides a templates for singleton class
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour{
    protected static T _instance;
    public static T Instance { 
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject singletonGO = new("Singleton_" + typeof(T).Name);
                    _instance = singletonGO.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual private void OnApplicationQuit() {
        Destroy(gameObject);
    }    
}
/// <summary>
/// any class that inherits from this class will  NOT be destroyed when the scene changes
/// use to create system class
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
{
   protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(Singleton<>), true)]
public class Singleton : Editor
{
     public override void OnInspectorGUI()
     {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Singleton", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("This will be destroyed when the scene changes");
     }
}


[CustomEditor(typeof(SingletonPersistent<>), true)]
public class SingletonPersistent : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Singleton Persistent", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("This AND ALL OF ITS CHILDREN will NOT be destroyed when the scene changes");
    }
}
#endif