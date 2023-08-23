using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Unit main class for controls and resource collection.
    /// </summary>
    public class UnitController : MonoBehaviour
    {
        public String PlayerNumber => "Player0";//TODO: Hardcorded for now, will be dyn when MP.

        private float m_moveSpeed = 3.5f;
        private WaitForEndOfFrame m_moveWait = new();
        private bool m_unitMoving = false;

        private NavGridElement m_currentNavGridElement;
        private GameObject[] m_path;
        private List<GameObject> m_pathList;
        private int m_pathIterator = 0;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.NavNode))
            {
                m_currentNavGridElement = other.GetComponent<NavGridElement>();
            }
        }


        public void CollectedResource()
        {
            //TODO: This needs to be either a signal or a Fcall to increase some player value.
            Debug.Log($"{gameObject.name} collected wood.");
        }


        public void MoveToDestination(GridController a_gridController, NavGridElement a_destinationElement/*Vector3 a_destination*/)
        {
            m_pathList = a_gridController.FindPath(m_currentNavGridElement.X,
                                      m_currentNavGridElement.Y,
                                      a_destinationElement.X,
                                      a_destinationElement.Y);
            m_path = m_pathList.ToArray();

            m_pathIterator = 0;
            var initialDestination = m_path[m_pathIterator].transform.position;

            if (!m_unitMoving || !SameDestination(initialDestination))
            {
                StopAllCoroutines();
                StartCoroutine(MoveCoroutine(initialDestination));
            }
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


        private IEnumerator MoveCoroutine(Vector3 a_destination)
        {
            m_unitMoving = true;
            Vector3 initialPosition = transform.position;
            a_destination.y = initialPosition.y;

            float distanceToDestination = Vector3.Distance(initialPosition, a_destination);
            float moveDuration = distanceToDestination / m_moveSpeed;

            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                transform.position = Vector3.Lerp(initialPosition, a_destination, t);

                elapsedTime += Time.deltaTime;
                yield return m_moveWait;
            }

            transform.position = a_destination;
            m_unitMoving = false;

            m_pathIterator++;
            if (m_path.Length > m_pathIterator)
            {
                StartCoroutine(MoveCoroutine(m_path[m_pathIterator].transform.position));
            }
        }
    }
}