using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    public static partial class GameAbuilder
    {
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
                instance.SetName($"Button {word}").SetTextInChildren(word);
                return instance;
            }
        }

        public static Transform Destroy(this Transform transform)
        {
            foreach (Transform child in transform) Object.Destroy(child.gameObject);
            return transform;
        }

        public static GameObject SetNewColor(this GameObject transform, string name)
        {
            Dictionary<string, Color32> color = new Dictionary<string, Color32>();
            color.Add("red", new Color32(245, 157, 0, 255));
            color.Add("green", new Color32(147, 219, 0, 255));
            color.Add("yellow", new Color32(243, 228, 71, 128));

            transform.GetOrAddComponent<Image>().color = color[name];
            return transform;
        }
    }
}
