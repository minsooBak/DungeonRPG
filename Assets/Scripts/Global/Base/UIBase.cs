using UnityEngine;

public class UIBase : MonoBehaviour
{
    public GameObject prefab;

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
