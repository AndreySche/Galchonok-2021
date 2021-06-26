using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Galchonok
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/GameA Settings", order = 1)]
    public class GameAsettings : ScriptableObject
    {
        public int Answers = 4;
        public int Questions = 20;
        
        public List<GameObject> Prefabs;
        public Ease QuestionEase = Ease.InOutQuint;
    }
}