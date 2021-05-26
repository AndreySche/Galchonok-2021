using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Galchonok
{
    class Cookies : MonoBehaviour
    {
        public static string showWarning;

        public static bool Init()
        {
            showWarning = GetKeyString("showWarning");
            return string.IsNullOrEmpty(showWarning);
        }

        static string GetKeyString(string _key)
        {
            string _value = PlayerPrefs.HasKey(_key) ? PlayerPrefs.GetString(_key) : "";
            //print($"{_key}: {_value}");
            return _value;
        }

        public static void SetString(string _key, string _value)
        {
            PlayerPrefs.SetString(_key, _value);
        }
    }
}
