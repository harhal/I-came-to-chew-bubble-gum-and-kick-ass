using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts
{
    public class AudioTrigger : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void TriggerAudio(AudioResource res)
        {
            var audioSource = GetComponent<AudioSource>();
            audioSource.resource = res;
            audioSource.Play();
        }
    }
}
