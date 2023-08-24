using System;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Basic Signal for action.
    /// </summary>
    public class BasicSignal
    {
        private Action m_action;

        public void Connect(Action a_action)
        {
            m_action += a_action;
        }

        public void Disconnect(Action a_action)
        {
            m_action -= a_action;
        }

        public void Emit()
        {
            m_action?.Invoke();
        }
    }

}
