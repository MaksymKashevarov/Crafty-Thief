using UnityEngine;

public class StorageInterfaceView : MonoBehaviour
{
    [SerializeField] private Transform _slotsParent;

    public Transform GetSlotsParent()
    {
        return _slotsParent;
    }
}
