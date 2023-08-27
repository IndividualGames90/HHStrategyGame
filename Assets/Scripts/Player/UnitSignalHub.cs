using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Hub to expose Unit signals. So unit structures can be retained and signalization separated.
    /// </summary>
    public class UnitSignalHub : MonoBehaviour
    {
        [HideInInspector] public bool Initialized = false;

        private List<BasicSignal<int>> m_resourceCollectedHub = new();
        private WaitForEndOfFrame m_delayedConnectWait = new();


        /// <summary> Register a signal to be emited. </summary>
        public void RegisterToHub(BasicSignal<int> a_registeringSignal)
        {
            m_resourceCollectedHub.Add(a_registeringSignal);
        }


        /// <summary> Connect a method to a broadcasting signal. </summary>
        public void ConnectToHub(Action<int> a_registeringMethod)
        {
            StartCoroutine(DelayedConnect(a_registeringMethod));
        }


        public IEnumerator DelayedConnect(Action<int> a_registeringMethod)
        {
            while (!Initialized)
            {
                yield return m_delayedConnectWait;
            }

            foreach (var signal in m_resourceCollectedHub)
            {
                signal.Connect(a_registeringMethod);
            }
        }
    }
}