using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    [SerializeField]
    private UnityEvent listenerEvent;

    private void OnEnable()
    {
        gameEvent.AddListener(this);  
    }

    private void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }

    public void OnEvent()
    {
        listenerEvent?.Invoke();
    }
}
