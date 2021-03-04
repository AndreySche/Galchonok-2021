using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace packages
{
    delegate void ButtonClick(int number);
    class Paginator : MonoBehaviour
    {
        #region Variables
        public List<Button> btns = null;
        public float seconds = 1f;

        static Transform area;
        static GameObject prefabBtn;

        ScrollRect scrollArea = null;
        float procent, startX, endX, time, sinInc, elasticPlus, elasticProcent, distance = 1f;
        bool movieAction;
        int right;
        #endregion        

        public static void Init(int _pages, ButtonClick _callback)
        {
            AttachButtons(_pages, _callback);
        }

        void Awake()
        {
            //Application.targetFrameRate = 60;
            area = transform.GetComponent<PaginatorConstruct>().GetTransfrom();
            prefabBtn = transform.GetComponent<PaginatorConstruct>().GetPrefab();

            elasticPlus = distance / 100f / 200f;
            scrollArea = gameObject.GetComponentInChildren<ScrollRect>();
            btns[0].onClick.AddListener(() => Go2end(false));
            btns[1].onClick.AddListener(() => Go2end(true));
        }

        void Go2end(bool _end)
        {
            movieAction = true;
            time = 0;
            procent = startX = scrollArea.horizontalNormalizedPosition;
            endX = _end ? (distance - startX) : startX;

            right = _end ? 1 : -1;
            elasticProcent = distance / endX;
            if (endX < distance / 100) movieAction = false;
        }

        void FixedUpdate()
        {
            if (!movieAction) return;

            sinInc = Mathf.Sin(time * 3.14f / seconds) * 3.14f / seconds;
            time += Time.fixedDeltaTime;
            procent += sinInc;

            if (sinInc < 0) { movieAction = false; procent -= sinInc; }

            scrollArea.horizontalNormalizedPosition = startX + (endX / 100f * procent + elasticPlus * procent / elasticProcent) * right;
        }

        static void AttachButtons(int _pages, ButtonClick _callback)
        {
            for (int i = 1; i <= _pages; i++)
            {
                var instance = Instantiate(prefabBtn);
                instance.transform.SetParent(area, false);
                instance.GetComponentInChildren<Text>().text = i + "";

                int _me = i;
                instance.GetComponentInChildren<Button>().onClick.AddListener(() => _callback(_me));
            }
        }
    }
}