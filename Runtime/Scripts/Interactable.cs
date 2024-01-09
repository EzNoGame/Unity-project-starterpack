using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void BeenSeen();
    public abstract void BeenUnSeen();
    public abstract void BeenIteracted(GameObject obj);
    public abstract void BeenUndone();
}
