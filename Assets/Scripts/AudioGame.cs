using UnityEngine;

namespace TestDankolab.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioGame : MonoBehaviour
    {
        public static AudioGame inst;

        AudioSource audioSource;

        private void Awake()
        {
            inst = this;
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        internal void ClickButton()
        {
            audioSource.Play();
        }
    }
}