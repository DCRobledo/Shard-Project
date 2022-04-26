using UnityEngine.Audio;
using UnityEngine;

namespace Shard.Lib.Custom
{
    [System.Serializable]
    public class Sound
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private AudioClip clip;

        [SerializeField] [Range(0f, 1f)]
        private float volume;
        [SerializeField] [Range(.1f, 3f)]
        private float pitch;

        [SerializeField]
        private bool loop;

        private AudioSource source;


        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public AudioClip GetClip()
        {
            return this.clip;
        }

        public void SetClip(AudioClip clip)
        {
            this.clip = clip;
        }

        public float GetVolume()
        {
            return this.volume;
        }

        public void SetVolume(float volume)
        {
            this.volume = volume;
        }

        public float GetPitch()
        {
            return this.pitch;
        }

        public void SetPitch(float pitch)
        {
            this.pitch = pitch;
        }

        public bool IsLoop()
        {
            return this.loop;
        }

        public void SetLoop(bool loop)
        {
            this.loop = loop;
        }

        public AudioSource GetSource()
        {
            return this.source;
        }

        public void SetSource(AudioSource source)
        {
            this.source = source;
            this.source.clip = this.clip;
            this.source.volume = this.volume;
            this.source.pitch = this.pitch;
            this.source.loop = this.loop;
        }

    }
}


