using System;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

namespace Shard.Lib.Custom
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer audioMixer;

        [SerializeField]
        private Sound[] sounds;

        public static AudioController Instance;


        private void Awake() {
            Instance = this;

            foreach (Sound sound in sounds) {
                AudioMixerGroup targetMixer = audioMixer.FindMatchingGroups("Master/" + sound.GetTargetMixer().ToString())[0];

                sound.SetSource(gameObject.AddComponent<AudioSource>(), targetMixer);
            }
                
        }

        private void Start() {
            FadeIn("BackgroundMusic", 4f);
        }


        public void Play(string name) {
            Sound mySound = Array.Find(sounds, sound => sound.GetName() == name);

            if(mySound == null)
                Debug.LogWarning($"WARNING: Sound {name} not found");
                
            else if (!mySound.GetSource().isPlaying)
                mySound.GetSource().Play();
        }

        public void Stop(string name) {
            Sound mySound = Array.Find(sounds, sound => sound.GetName() == name);

            if(mySound == null)
                Debug.LogWarning($"WARNING: Sound {name} not found");
            else
                mySound.GetSource().Stop();
        }

        public void Pause(string name) {
            Sound mySound = Array.Find(sounds, sound => sound.GetName() == name);

            if(mySound == null)
                Debug.LogWarning($"WARNING: Sound {name} not found");
            else
                mySound.GetSource().Pause();
        }

        public void UnPause(string name) {
            Sound mySound = Array.Find(sounds, sound => sound.GetName() == name);

            if(mySound == null)
                Debug.LogWarning($"WARNING: Sound {name} not found");
            else
                mySound.GetSource().UnPause();
        }


        public void FadeIn(String soundName, float fadeDuration) {
            Sound mySound = Array.Find(sounds, sound => sound.GetName() == soundName);

            if(mySound == null)
                Debug.LogWarning($"WARNING: Sound {name} not found");
            else 
                StartCoroutine(FadeInCoroutine(mySound, fadeDuration));
        }

        public void FadeOut(String soundName, float fadeDuration) {
            Sound mySound = Array.Find(sounds, sound => sound.GetName() == soundName);

            if(mySound == null)
                Debug.LogWarning($"WARNING: Sound {name} not found");
            else
                StartCoroutine(FadeOutCoroutine(mySound, fadeDuration));
        }

        private IEnumerator FadeInCoroutine(Sound sound, float fadeDuration)
        {
            sound.GetSource().Play();

            float currentTime = 0;
            float start = sound.GetSource().volume;

            while (currentTime < fadeDuration)
            {
                currentTime += Time.deltaTime;
                sound.GetSource().volume = Mathf.Lerp(0, start, currentTime / fadeDuration);
                yield return null;
            }

            yield break;
        }

        private IEnumerator FadeOutCoroutine(Sound sound, float fadeDuration)
        {
            float currentTime = 0;
            float start = sound.GetSource().volume;

            while (currentTime < fadeDuration)
            {
                currentTime += Time.deltaTime;
                sound.GetSource().volume = Mathf.Lerp(start, 0, currentTime / fadeDuration);
                yield return null;
            }

            sound.GetSource().Stop();

            yield break;
        }
    }
}



