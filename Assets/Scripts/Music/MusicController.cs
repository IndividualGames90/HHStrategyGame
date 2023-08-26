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
        [SerializeField] private CanvasEventHub m_canvasEventHub;


        private void Awake()
        {
            m_canvasEventHub.VolumeChanged.Connect(OnVolumeChanged);
            PhotonController.JoinedRoom.Connect(PlayGameClip);
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
            m_source.clip = m_gameClip;
            m_source.Play();
        }


        public void OnVolumeChanged(float a_newVolume)
        {
            m_source.volume = a_newVolume;
        }
    }
}