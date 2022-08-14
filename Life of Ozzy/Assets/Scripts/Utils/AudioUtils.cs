using UnityEngine;

namespace LifeOfOzzy.Utils
{
    public class AudioUtils
    {
        public const string SfxSourceTag = "SfxAudioSource";

        public static AudioSource FindSfxSource()
        {
            return GameObject.FindWithTag(SfxSourceTag).GetComponent<AudioSource>();
        }
    }

}
