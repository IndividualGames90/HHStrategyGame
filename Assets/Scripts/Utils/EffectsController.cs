using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Makeshift effects controller/pool for this case.
    /// </summary>
    public class EffectsController : MonoBehaviour, IPool
    {
        [SerializeField] private GameObject m_effectPrefab;

        public static EffectsController Instance { get; private set; }

        private int m_initialPoolSize = 10;
        private float m_returnDelay = 2.0f;

        private List<GameObject> pooledEffects = new List<GameObject>();


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public void Initialize()
        {
            for (int i = 0; i < m_initialPoolSize; i++)
            {
                GameObject effect = Instantiate(m_effectPrefab);
                effect.SetActive(false);
                pooledEffects.Add(effect);
            }
        }


        public GameObject Retrieve()
        {
            GameObject effect = pooledEffects.FirstOrDefault(e => !e.activeInHierarchy);

            if (effect == null)
            {
                effect = Instantiate(m_effectPrefab);
                pooledEffects.Add(effect);
            }

            effect.SetActive(true);
            StartCoroutine(Return(effect));

            return effect;
        }


        private IEnumerator Return(GameObject effect)
        {
            yield return new WaitForSeconds(m_returnDelay);
            effect.SetActive(false);
        }
    }
}