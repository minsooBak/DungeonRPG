using UnityEngine;

public class UIBase : MonoBehaviour
{

    public void Start()
    {

    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
