namespace Game.Core.Interactable
{
    public interface IInteractable
    {
        Item GetItem();
        bool IsInteractable();

        bool isTool();

    }

}

