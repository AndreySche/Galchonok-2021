using UnityEngine;

class PaginatorConstruct : MonoBehaviour
{
    [SerializeField] Transform area = null;
    [SerializeField] GameObject prefabBtn = null;

    public Transform GetTransfrom() { return area; }
    public GameObject GetPrefab() { return prefabBtn; }
}
