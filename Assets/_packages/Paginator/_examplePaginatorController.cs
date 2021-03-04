using UnityEngine;

namespace packages
{
    class _examplePaginatorController : MonoBehaviour
    {
        void Start() => Paginator.Init(25, ClickMe);
        public void ClickMe(int _number) => print($"number={_number}");
    }
}