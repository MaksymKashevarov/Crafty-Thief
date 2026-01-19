namespace Game.Core.UI
{
    using Game.Core.SceneControl;
    using UnityEngine;

    public interface ISceneConnected
    {
        void AssignScene(SceneData scene);
        SceneData GetAssignedScene();

    }

}
