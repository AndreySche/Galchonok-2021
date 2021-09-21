using TypeOne;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSet : MonoBehaviour
{
    private Toggle _me;

    public void Set(Cookie name)
    {
        Cookies.Set(name, (_me.isOn ? 1 : -1));
    }

    public void Init(Cookie name, int defaultValue)
    {
        _me = GetComponent<Toggle>();
        int cookies = Cookies.Get(name, defaultValue);
        _me.isOn = cookies == 1;
        _me.onValueChanged.AddListener(delegate { Set(name); });
    }
}