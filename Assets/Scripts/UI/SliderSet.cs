using TypeOne;
using UnityEngine;
using UnityEngine.UI;

public class SliderSet : MonoBehaviour
{
    [SerializeField] private Text _text;
    private Slider _me;
    private Cookie _name;

    public void Set()
    {
        _text.text = _me.value.ToString();
        Cookies.Set(_name, (int) _me.value);
    }

    public void Init(Cookie name, int defaultValue, int minValue, int maxValue, bool wholeNumbers)
    {
        _name = name;
        _me = gameObject.GetComponent<Slider>();
        int cookies = Cookies.Get(name, defaultValue);
        _me.minValue = minValue;
        _me.maxValue = maxValue;
        _me.wholeNumbers = wholeNumbers;
        _me.value = cookies;
        _text.text = cookies.ToString();
        
    }
}