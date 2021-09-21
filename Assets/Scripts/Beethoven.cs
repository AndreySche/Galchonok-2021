using UnityEngine;


public class Beethoven : MonoBehaviour /*, IPointerDownHandler, IPointerUpHandler*/
{
    private AudioSource _source;
    [SerializeField] private AudioClip[] _sounds;

    private void Start()
    {
        _source = gameObject.AddComponent<AudioSource>();
    }

    /*public void OnPointerDown(PointerEventData data)
    {
        Click();
    }*/


    public void Click()
    {
        _source.clip = _sounds[0];
        _source.Play();
    }

    public void Result(bool correct)
    {
        _source.clip = _sounds[correct ? 1 : 2];
        _source.Play();
#if UNITY_ANDROID && UNITY_IPHONE
                Handheld.Vibrate();
#endif
    }

    //public void OnPointerUp(PointerEventData eventData) { }
}