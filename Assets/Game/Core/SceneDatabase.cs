using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Config/Scene Database")]
public class SceneDatabase : ScriptableObject
{
    [SerializeField] private AssetReference _sourceScene;
    [SerializeField] private List<AssetReference> _devScenes;
    [SerializeField] private List<AssetReference> _lowLevels;

    private Dictionary<string, List<AssetReference>> _referenceBundle = new();

    public void AssembleDataBase()
    {
        AssembleReferences();
    }

    private void AssembleReferences()
    {
        Debug.Log("Assembling Scenes!");
        _referenceBundle["DevScenes"] = _devScenes;
        _referenceBundle["Low-Risk"] = _lowLevels;
        foreach (string key in _referenceBundle.Keys)
        {
            if (key == null)
            {
                Debug.LogAssertion("Key is Invalid!");
                break;
            }
            Debug.Log($"Bundle contains: {key}");
        }
    }

    public List<AssetReference> GetDevScenes()
    {
        return _devScenes;
    }

    public AssetReference GetSourceScene()
    {
        return _sourceScene;
    }
}