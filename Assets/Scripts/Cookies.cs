using UnityEngine;


public static class Cookies
{
    public static int Get(Cookie name, int defaultValue)
    {
        var cookies = PlayerPrefs.GetInt($"cookie{name}");
        //Debug.Log($"get cookie {name}={cookies}; (int)default={defaultValue}");
        cookies = cookies == 0 ? defaultValue : cookies;
        return cookies;
    }

    public static void Set(Cookie name, int value)
    {
        //Debug.Log($"set cookie {name}={value}");
        PlayerPrefs.SetInt($"cookie{name}", value);
    }
}

public enum Cookie
{
    Answers,
    Questions,
    Hint,
    Mp3
}