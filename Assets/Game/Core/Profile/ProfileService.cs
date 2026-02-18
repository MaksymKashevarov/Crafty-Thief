using System;

namespace Game.Core.Profile 
{
    public class ProfileService
    {
        public PlayerProfile InitializeProfile()
        {
            return CreateNew();
        }

        private PlayerProfile CreateNew()
        {
            PlayerProfile profile = new PlayerProfile();
            DevLog.Log("Initializing profile....", this);
            profile.ProfileVersion = 1;
            profile.PlayerId = Guid.NewGuid().ToString();
            profile.CreatedAtUtc = DateTime.UtcNow.ToString("o");
            profile.LastSaveUtc = DateTime.UtcNow.ToString("o");

            return profile;
        }
    }

}

   