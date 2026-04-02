namespace Game.Utility
{
    public static class RandomTool
    {
        public static int RollInt(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }

}

