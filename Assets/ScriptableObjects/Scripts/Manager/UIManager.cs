using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UIManager", menuName = "Data/UIManager")]
public class UIManager : ScriptableObject
{
    public List<UIBase> uiBases = new();
    public GameObject uiBasePrefab;
    private GameObject _obj;

    public T GetUI<T>() where T : UIBase
    {
        foreach(UIBase ui in uiBases)
        {
            if(ui is T)
            {
                return ui as T;
            }
        }

        if (_obj == null)
        {
            CreateBasePrefab();
        }

        T data = _obj.GetComponent<T>();
        T result = CreateUIPrefab(data.prefab, _obj.transform).GetComponent<T>();
        Destroy(data);
        uiBases.Add(result);
        return result;
    }

    public void RemoveUI<T>(T data) where T : UIBase
    {
        Destroy(_obj.GetComponent<T>());
        uiBases.Remove(data);
    }

    public void Clear()
    {
        uiBases.Clear();
    }

    private void CreateBasePrefab()
    {
        _obj = Instantiate(uiBasePrefab);
    }

    private GameObject CreateUIPrefab(GameObject obj, Transform transform)
    {
        return Instantiate(obj, transform);
    }
}
