using System;

namespace Game.Core.Profile 
{
    public static class ProfileService
    {
        public static PlayerProfile InitializeProfile()
        {
            return CreateNew();
        }

        private static PlayerProfile CreateNew()
        {
            PlayerProfile profile = new PlayerProfile();

            profile.ProfileVersion = 1;
            profile.PlayerId = Guid.NewGuid().ToString();
            profile.CreatedAtUtc = DateTime.UtcNow.ToString("o");
            profile.LastSaveUtc = DateTime.UtcNow.ToString("o");

            return profile;
        }
    }

}

   