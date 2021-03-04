using UnityEngine;
using UnityEngine.UI;

namespace Galchonok
{
    public static partial class BuilderExtensions
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
    }
}
