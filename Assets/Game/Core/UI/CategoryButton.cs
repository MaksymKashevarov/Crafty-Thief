using Game.Core.ServiceLocating;
using Game.Core.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CategoryButton : MonoBehaviour, IUIElement, IButton
{
    [SerializeField] private TextMeshProUGUI _categoryText;
    private string _categoryTextString;
    private UIController _controller;
    private GameObject _instance;
    private IUIElement _parent;

    public void Activate()
    {
        Debug.Log("Check");
        Debug.LogWarning(gameObject.scene.name);
    }
    public void SetText(string input)
    {
        _categoryTextString = input;

        if (_categoryText != null)
            _categoryText.text = input;

        Debug.Log($"[{name}] SetText called: {input}");
    }
    public void CollectChildElements()
    {
        return;
    }

    private void Awake()
    {
        Container.tContainer.RegisterAsTElement(this);
    }

    private void Start()
    {
        /*
        if (_categoryText == null)
        {
            Debug.LogAssertion($"[{this.name}] Missing Category Text");
            return;
        }
        if (_categoryTextString == null)
        {
            Debug.LogAssertion($"[{this.name}] Missing string");
            return;
        }
        if (_categoryTextString.Length == 0)
        {
            Debug.LogAssertion($"[{this.name}] text is empty");
            return;
        }
        _categoryText.text = _categoryTextString;
        */
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
        Debug.Log($"Recieved Parent: {element}");
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
}
