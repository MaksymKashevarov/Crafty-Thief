using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Config/Scene Database")]
public class SceneDatabase : ScriptableObject
{
    [SerializeField] private AssetReference _sourceScene;
    [SerializeField] private List<AssetReference> _devScenes;

    public List<AssetReference> GetDevScenes()
    {
        return _devScenes;
    }

    public AssetReference GetSourceScene()
    {
        return _sourceScene;
    }
}