using System;
using System.Collections.Generic;

namespace Data.Storage.Dynamic
{
    [Serializable]
    public class ProfileData
    {
        private string nickname;
        private string userId;
        private int profileImageNumber;

        private int currentEXP;
        private int level;

        private string lastAccessTime;

        public ProfileData() { }

        public ProfileData(string nickname, string userId, int profileImageNumber, int currentEXP, int level, string lastAccessTime)
        {
            this.nickname = nickname;
            this.userId = userId;
            this.profileImageNumber = profileImageNumber;
            this.currentEXP = currentEXP;
            this.level = level;
            this.lastAccessTime = lastAccessTime;
        }

        public ProfileData(ProfileData other)
        {
            this.nickname = other.Nickname;
            this.userId = other.UserId;
            this.profileImageNumber = other.ProfileImageNumber;
            this.currentEXP = other.CurrentEXP;
            this.level = other.Level;
            this.lastAccessTime = other.LastAccessTime;
        }

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> newProfileData = new Dictionary<string, object>
            {
                { "Nickname", this.nickname },
                { "UserId", this.userId },
                { "ProfileImageNumber", this.profileImageNumber },
                { "CurrentEXP", this.currentEXP },
                { "Level", this.level },
                { "LastAccessTime", this.lastAccessTime },
            };

            return newProfileData;
        }

        public string Nickname { get => nickname; set => nickname = value; }
        public string UserId { get => userId; set => userId = value; }
        public int ProfileImageNumber { get => profileImageNumber; set => profileImageNumber = value; }

        public int CurrentEXP { get => currentEXP; set => currentEXP = value; }
        public int Level { get => level; set => level = value; }

        public string LastAccessTime { get => lastAccessTime; set => lastAccessTime = value; }
    }
}
