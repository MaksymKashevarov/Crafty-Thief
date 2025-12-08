using Game.Core.ServiceLocating;
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
