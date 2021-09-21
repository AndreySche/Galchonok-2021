using System.Collections.Generic;
using UnityEngine;

namespace TypeOne
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Settings TypeOne", order = 1)]
    public class Settings : ScriptableObject
    {
        public int Answers = 4;
        public int Questions = 20;
        public int Hint = 0;
        public int Mp3 = 1;
        public GameObject AnswerPrefab;
        public List<Sprite> Images;
    }
}