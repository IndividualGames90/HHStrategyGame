using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Animator for units.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class UnitAnimator : MonoBehaviour
    {
        private Animator m_animator;
        private bool m_walking = false;


        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }


        public void ToggleWalking()
        {
            m_walking = !m_walking;
            m_animator.SetBool("Walking", m_walking);
        }


        public void StopWalking()
        {
            m_walking = false;
            m_animator.SetBool("Walking", m_walking);
        }
    }
}