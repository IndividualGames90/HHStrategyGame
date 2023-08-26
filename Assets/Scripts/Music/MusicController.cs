using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Very primitive music controls for predefined tracks.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioClip m_loadingClip;
        [SerializeField] private AudioClip m_gameClip;
        [SerializeField] private AudioSource m_source;


        private void Awake()
        {
            PlayLoadingClip();
        }


        public void PlayLoadingClip()
        {
            m_source.Stop();
            m_source.clip = m_loadingClip;
            m_source.Play();
        }


        public void PlayGameClip()
        {
            m_source.Stop();
            m_source.clip = m_loadingClip;
            m_source.Play();
        }
    }
}