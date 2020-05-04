using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManaging
{
    public class MusicManager : MonoBehaviour
    {
        public AudioSource startMusic;
        public AudioSource loopMusic;
        public AudioSource combatMusic;
        public bool isOnFight;

        public float volume;

        static float time;

        void Start()
        {
            volume = 0.5f;
            startMusic.volume = volume;
            loopMusic.volume = volume;
            time = 0f;           
            startMusic.Play();
            StartCoroutine(EndOfTheStart());
        }

        void Update()
        {
            //déclanchement/arret de la musique de combat
            if (isOnFight && combatMusic.volume != volume)
                combatMusic.volume = Mathf.Lerp(0f, volume, time);

            if (!isOnFight && combatMusic.volume != 0f)
                combatMusic.volume = Mathf.Lerp(volume, 0f, time);

            if (time > 1f)
            {
                time = 0f;
            }

            time += 0.2f * Time.deltaTime;
    
        }
        IEnumerator EndOfTheStart()
        {
            yield return new WaitForSecondsRealtime(startMusic.clip.length);
            loopMusic.Play();
            combatMusic.Play();
        }
    }

}
