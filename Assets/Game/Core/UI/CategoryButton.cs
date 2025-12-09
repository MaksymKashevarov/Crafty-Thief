using Game.Core.ServiceLocating;
using Game.Core.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CategoryButton : MonoBehaviour, IUIElement, IButton
{
    [SerializeField] private TextMeshProUGUI _categoryText;
    private string _categoryTextString = string.Empty;
    [SerializeField] private UIController _controller;
    private GameObject _instance;
    private IUIElement _parent;

    public void Activate()
    {
        Debug.Log("Check");
        Debug.LogWarning(gameObject.scene.name);
    }
    public void SetText(string input)
    {
        Debug.Log($"[{this.name}] Setting text: [{input}] To:");
       _categoryTextString = input;
    }
    public void CollectChildElements()
    {
        return;
    }

    private void Awake()
    {
        Container.tContainer.RegisterAsTElement(this);
        Debug.Log(_categoryTextString);
    }
    private void Start()
    {
        Debug.LogWarning($"[{this.name}] Parent: [{_parent}]");
        _categoryText.text = _categoryTextString;   
    }

    public List<IUIElement> GetChildElements()
    {
        return null;
    }

    public GameObject GetInstance()
    {
        return _instance;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

    public void SetController(UIController controller)
    {
        _controller = controller;
        if (_controller == null)
        {
            return;
        }
    }

    public void SetInstance(GameObject instance)
    {
        _instance = instance;
    }

    public void SetParent(IUIElement element)
    {
        Debug.LogWarning($"[{this.name}] Recieved Parent: {element}");
        _parent = element;
    }

    public void Terminate()
    {
        _controller.DestroyElement(this);
    }

    public TextMeshProUGUI GetText()
    {
        return _categoryText;
    }

    public TextMeshProUGUI GetTextComponent()
    {
        Debug.Log($"[{this.name}] Returning Text Component: [{_categoryText}]");
        return _categoryText;
    }
}
