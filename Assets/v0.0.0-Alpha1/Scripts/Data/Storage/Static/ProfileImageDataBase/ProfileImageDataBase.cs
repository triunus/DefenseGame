using UnityEngine;

namespace Data.Storage.Static
{
    public class ProfileImageDataBase
    {
        private ProfileImageDataGroup profileImageDataGroup;

        private void ProfileImageDataLoad()
        {
            this.profileImageDataGroup = Resources.Load<ProfileImageDataGroup>("ScriptableObject/ProfileImage/ProfileImageDataGroup");
        }

        public void ProfileImageDataClear()
        {
            this.profileImageDataGroup = null;
        }

        public ProfileImageData GetProfileImageData(int profileNumber)
        {
            if (this.profileImageDataGroup == null) this.ProfileImageDataLoad();

            return this.profileImageDataGroup.GetProfileImageData(profileNumber);
        }
    }
}