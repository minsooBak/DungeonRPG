using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName ="UIManager", menuName = "Data/UIManager")]
public class UIManager : ScriptableObject
{
    public List<UIBase> uiBases = new();
    public GameObject uiBasePrefab;
    private GameObject _obj;
    [SerializeField] private SpriteAtlas _atlas;

    public T GetUI<T>() where T : UIBase
    {
        foreach (UIBase ui in uiBases)
        {
            if (ui is T)
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

    public bool TryGetUI<T>(out T data) where T: UIBase
    {
        foreach (UIBase ui in uiBases)
        {
            if (ui is T)
            {
                data = ui as T;
                return true;
            }
        }
        data = null;
        return false;
    }

    public void Clear()
    {
        uiBases.Clear();
    }

    public Sprite GetSprite(string name)
    {
        return _atlas.GetSprite(name);
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
