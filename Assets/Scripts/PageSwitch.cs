using UnityEngine;
using System.Collections.Generic;

namespace Galchonok
{
    class PageSwitch
    {
        public Dictionary<Pages, GameObject> Prefabs { get; } = new Dictionary<Pages, GameObject>();
        Curtain _curtainSwitch;
        Transform _area;

        public PageSwitch(List<GameObject> list, Transform area, GameObject curtain, float duration)
        {
            _area = area;
            _curtainSwitch = new Curtain(curtain, duration);
            foreach (GameObject child in list)
            {
                if (System.Enum.IsDefined(typeof(Pages), child.name))
                {
                    Prefabs.Add((Pages)System.Enum.Parse(typeof(Pages), child.name), child);
                }
                else Debug.Log($"В enum.Pages не добавлена страница <{child.name}>");
            }
        }
        
        public GameObject OpenPage(Pages pageName)
        {
            _area.Destroy();
            _curtainSwitch.Show(false);
            return AttachPage(pageName);
        }

        public GameObject AttachPage(Pages pageName)
        {
            var instance = Object.Instantiate(Prefabs[pageName]);
            instance.transform.SetParent(_area, false);
            instance.name = pageName.ToString();
            return instance;
        }

        public void CurtainShow() => _curtainSwitch.Show(true);

        // временная
        public void PrintPageList()
        {
            foreach (var child in Prefabs) Debug.Log(child.Key);
        }
    }
}