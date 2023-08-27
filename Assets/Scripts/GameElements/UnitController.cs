using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Unit main class for controls and resource collection.
    /// </summary>
    public class UnitController : MonoBehaviour, ISelectable
    {
        public BasicSignal<int> ResourceCollected = new();

        private WaitForEndOfFrame m_moveWait = new();
        private List<GameObject> m_pathList;

        private NavGridElement m_currentNavGridElement;
        private ResourceController m_resourceController;
        private FormationController m_formationController;

        private bool m_unitMoving = false;
        private int m_pathIterator = 0;
        private int m_currentFormationIndex = -1;
        private const float c_moveSpeed = 2.7f;


        public void Init(ResourceController a_resourceController,
                         FormationController a_formationController)
        {
            m_resourceController = a_resourceController;
            m_formationController = a_formationController;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.NavNode))
            {
                m_currentNavGridElement = other.GetComponent<NavGridElement>();
            }
        }


        public void CollectedResource()
        {
            ResourceCollected.Emit(m_resourceController.ResourceCollected());
        }


        public void MoveToDestination(GridController a_gridController,
                                      NavGridElement a_destinationElement)
        {
            var pathCache = a_gridController.FindPath(m_currentNavGridElement.X,
                                      m_currentNavGridElement.Y,
                                      a_destinationElement.X,
                                      a_destinationElement.Y);

            var noPathFound = pathCache == null;
            if (noPathFound)
            {
                return;
            }

            m_pathList = pathCache;
            m_pathIterator = 0;
            var initialDestination = m_pathList[m_pathIterator].transform.position;

            if (!m_unitMoving || !SameDestination(initialDestination))
            {
                StopAllCoroutines();
                StartCoroutine(MoveDownThePathList());
            }
        }


        private IEnumerator MoveDownThePathList()
        {
            m_unitMoving = true;
            m_formationController.ReleasePosition(m_currentFormationIndex);
            var tuple = m_formationController.ReserveFirstAvailableOrDefault();

            while (m_pathList.Count > m_pathIterator)
            {
                Vector3 initialPosition = transform.position;
                Vector3 currentDestination;

                var reachedFinalNode = m_pathList.Count - 1 == m_pathIterator;
                var tupleIsValid = tuple.Item1 != -1;

                if (reachedFinalNode && tupleIsValid)
                {
                    m_currentFormationIndex = tuple.Item1;
                    currentDestination = tuple.Item2.position;
                }
                else
                {
                    currentDestination = m_pathList[m_pathIterator].transform.position;
                }

                currentDestination.y = initialPosition.y;

                float distanceToDestination = (initialPosition - currentDestination).sqrMagnitude;
                float moveDuration = distanceToDestination / (c_moveSpeed * c_moveSpeed);
                float elapsedTime = 0f;

                while (elapsedTime < moveDuration)
                {
                    float t = elapsedTime / moveDuration;
                    transform.position = Vector3.Lerp(initialPosition, currentDestination, t);

                    elapsedTime += Time.deltaTime;
                    yield return m_moveWait;
                }

                m_pathIterator++;
                transform.position = currentDestination;
            }

            m_formationController.ReleasePosition(m_currentFormationIndex);
            m_unitMoving = false;
        }


        private bool SameDestination(Vector3 a_destination)
        {
            var currentPosition = transform.position;
            if (a_destination.x == currentPosition.x &&
                a_destination.x == currentPosition.y &&
                a_destination.x == currentPosition.z)
            {
                return true;
            }

            return false;
        }


        public GameObject GameObject()
        {
            return gameObject;
        }
    }
}