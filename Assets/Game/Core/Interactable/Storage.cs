namespace Game.Core.Interactable
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "Storage", menuName = "Storage")]
    public class Storage : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Sprite itemImage;
        [SerializeField] private int storageSlots;

        public int GetStorageSlots()
        {
            return storageSlots;
        }
    }

}

