using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UIManager", menuName = "Data/UIManager")]
public class UIManager : ScriptableObject
{
    public List<UIBase> uIBases = new();
    public GameObject uiBasePrefab;
    private GameObject obj;
    public T GetUI<T>() where T : UIBase
    {
        foreach(UIBase u in uIBases)
        {

        }
        if (obj == null)
            obj = Instantiate(uiBasePrefab);

        T data = obj.GetComponentInChildren<T>();
        if(obj.TryGetComponent<T>(out data))
        {
            return data;
        }

        data = obj.AddComponent<T>();
        return data;
    }

    public void RemoveUI<T>(T data) where T : UIBase
    {
        Destroy(obj.GetComponent<T>());
        uIBases.Remove(data);
    }

    public void Clear()
    {
        uIBases.Clear();
    }
}
