namespace Game.Core.Interactable
{
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(fileName = "Interactable", menuName = "Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Sprite itemImage;

        public string GetItemName()
        {
            return itemName;
        }

        public GameObject GetPrefab()
        {
            return itemPrefab;
        }

        public Sprite GetImage()
        {
            return itemImage;
        }
    }

}

