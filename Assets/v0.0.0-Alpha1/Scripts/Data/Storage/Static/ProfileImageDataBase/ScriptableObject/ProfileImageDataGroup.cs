using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Storage.Static
{
    [Serializable]
    [CreateAssetMenu(fileName = "ProfileImageDataGroup", menuName = "ScriptableObject/ProfileImage/ProfileImageDataGroup")]
    public class ProfileImageDataGroup : ScriptableObject
    {
        [SerializeField]
        private List<ProfileImageData> profileImageDatas;

        public ProfileImageData GetProfileImageData(int profileNumber)
        {
            ProfileImageData returnProfileImageData = null;

            for (int i = 0; i < profileImageDatas.Count; ++i)
            {
                if (i == profileNumber)
                    returnProfileImageData = this.profileImageDatas[i];
            }

            return returnProfileImageData;
        }
    }

    [Serializable]
    public class ProfileImageData
    {
        [SerializeField]
        private int profileImageNumber;
        [SerializeField]
        private Texture2D profileImageTexture2D;

        public int ProfileImageNumber { get => profileImageNumber; }
        public Texture2D ProfileImageTexture2D { get => profileImageTexture2D; }
    }
}