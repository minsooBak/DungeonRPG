using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Event", menuName = "Data/Event")]
[System.Serializable]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> _listener = new();

    public void PostEvent()
    {
        foreach(GameEventListener listener in _listener)
        {
            listener.OnEvent();
        }
    }

    public void AddListener(GameEventListener listener)
    {
        _listener.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        _listener.Remove(listener);
    }
}
