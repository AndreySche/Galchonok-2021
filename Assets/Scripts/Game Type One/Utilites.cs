using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static partial class Utilites
{
    public static GameObject SetName(this GameObject gameObject, string name)
    {
        gameObject.name = name;
        return gameObject;
    }

    public static GameObject AddClick(this GameObject gameObject, string name)
    {
        gameObject.GetOrAddComponent<Button>().onClick.AddListener(() => Debug.Log(name));
        return gameObject;
    }

    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        var result = gameObject.GetComponent<T>();
        if (!result)
        {
            result = gameObject.AddComponent<T>();
        }

        return result;
    }

    public static GameObject SetTextInChildren(this GameObject gameObject, string word)
    {
        var result = gameObject.GetComponentInChildren<Text>();
        if (result) gameObject.GetComponentInChildren<Text>().text = word;
        return gameObject;
    }

    public static GameObject Attach(this Transform transform, string word, GameObject prefab)
    {
        {
            GameObject instance = Object.Instantiate(prefab);
            instance.transform.SetParent(transform, false);
            instance.SetName(word).SetTextInChildren(word);
            instance.SetName(word);
            return instance;
        }
    }

    public static Transform Destroy(this Transform transform)
    {
        foreach (Transform child in transform) Object.Destroy(child.gameObject);
        return transform;
    }

    public static GameObject SetNewColor(this GameObject transform, rgb paint)
    {
        Dictionary<rgb, Color32> color = new Dictionary<rgb, Color32>();
        color.Add(rgb.Red, new Color32(255, 191, 79, 255));
        color.Add(rgb.Green, new Color32(169, 255, 60, 255));
        color.Add(rgb.LightGreen, new Color32(140, 147, 87, 80));
        color.Add(rgb.Rose, new Color32(255, 144, 219, 255));
        //color.Add(rgb.LightYellow, new Color32(255, 242, 209, 255));
        color.Add(rgb.White, new Color32(153, 226, 255, 255));

        transform.GetOrAddComponent<Image>().color = color[paint];
        return transform;
    }

    public static List<T> RandomList<T>(this IEnumerable<T> list)
    {
        //Debug.Log(String.Join(", ", _questionsIndexList.ToArray()));
        return list.OrderBy(arg => System.Guid.NewGuid()).Take(list.Count()).ToList();
    }

    //public static string FirstLetterToUp(this string str) => char.ToUpper(str[0]) + str.Substring(1);
}

public enum rgb
{
    Rose,
    //LightYellow,
    Red,
    Green,
    LightGreen,
    White,
    //Blue
}