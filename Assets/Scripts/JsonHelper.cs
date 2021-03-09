using UnityEngine;
//using System.Globalization;
//using System.Text.RegularExpressions;

public class JsonHelper
{
    public static List FromJson<List>(string json)
    {
        if (json.Substring(0, 1) == "{") json = $"[{json}]";    // andreySche
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<List> wrapper = JsonUtility.FromJson<Wrapper<List>>(newJson);
        return wrapper.array;
    }

    public static string ToJson<List>(List array, bool fullView = false)
    {
        Wrapper<List> wrapper = new Wrapper<List>();
        wrapper.array = array;
        return JsonUtility.ToJson(wrapper, fullView);
    }

    [System.Serializable]
    private class Wrapper<List>
    {
        //public T[] array;
        public List array;
    }

    //public static string Unicode(string result)
    //{
    //    Regex rx = new Regex(@"\\[uU]([0-9A-F]{4})");
    //    return rx.Replace(result, match => ((char)int.Parse(match.Value.Substring(2), NumberStyles.HexNumber)).ToString());
    //}
}
