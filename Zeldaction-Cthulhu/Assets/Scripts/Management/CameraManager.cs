using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;
using Cinemachine;
using UnityEngine.Events;

namespace Game
{
    public class CameraManager : Singleton<CameraManager>
    {
        CinemachineBasicMultiChannelPerlin MainCamNoise;
        CinemachineBasicMultiChannelPerlin ShadowCamNoise;

        private void Awake()
        {
            MakeSingleton(true);

        }

        // assignation des noises pour le cameraShake
        void Start()
        {
            MainCamNoise = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>().
                                                 GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            
        }

        //Coroutine à appeler pour le cameraShake de la MainCaméra
        public IEnumerator MainCameraShake(float amplitude, float frequency, float time)
        {
            MainCamNoise.m_AmplitudeGain = amplitude;
            MainCamNoise.m_FrequencyGain = frequency;

            yield return new WaitForSecondsRealtime(time);

            MainCamNoise.m_AmplitudeGain = 0f;
            MainCamNoise.m_FrequencyGain = 0f;
        }
        //Coroutine à appeler pour le cameraShake de la ShadowCaméra
        public IEnumerator ShadowCameraShake(float amplitude, float frequency, float time)
        {
            ShadowCamNoise = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>().
                                                   GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            ShadowCamNoise.m_AmplitudeGain = amplitude;
            ShadowCamNoise.m_FrequencyGain = frequency;

            yield return new WaitForSecondsRealtime(time);

            ShadowCamNoise.m_AmplitudeGain = 0f;
            ShadowCamNoise.m_FrequencyGain = 0f;
        }
    }
}


