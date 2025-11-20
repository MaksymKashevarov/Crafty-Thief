using Game.Core.DI;
using UnityEngine;

public class CamSpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        Container.Register(this);
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
