using System.Collections.Generic;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Selection Box that can detect elements on the screen within bounds.
    /// </summary>
    public class SelectionBox
    {
        private float m_width;
        private float m_height;

        private RectTransform m_selectionBox;

        private Vector2 m_anchoredPositionAdjustment;
        private Vector2 m_sizeDeltaAdjustment;
        private Bounds m_bounds;

        private Camera m_mainCamera;

        private List<ISelectable> m_selected = new();


        public SelectionBox(RectTransform a_selectionBox)
        {
            m_selectionBox = a_selectionBox;
            m_mainCamera = Camera.main;
        }


        public List<ISelectable> StartSelecting(Vector2 a_startPosition,
                                                Vector2 a_endPosition,
                                                List<ISelectable> a_selectables)
        {
            ResizeSelectionBox(a_startPosition, a_endPosition);
            return CheckSelectionBox(a_selectables);
        }


        private void ResizeSelectionBox(Vector2 a_startPosition, Vector2 a_endPosition)
        {
            m_width = a_endPosition.x - a_startPosition.x;
            m_height = a_endPosition.y - a_startPosition.y;

            m_anchoredPositionAdjustment.x = m_width / 2;
            m_anchoredPositionAdjustment.y = m_height / 2;

            m_sizeDeltaAdjustment.x = Mathf.Abs(m_width);
            m_sizeDeltaAdjustment.y = Mathf.Abs(m_height);

            m_selectionBox.anchoredPosition = a_startPosition + m_anchoredPositionAdjustment;
            m_selectionBox.sizeDelta = m_sizeDeltaAdjustment;

            m_bounds.center = m_selectionBox.anchoredPosition;
            m_bounds.size = m_selectionBox.sizeDelta;
        }


        private List<ISelectable> CheckSelectionBox(List<ISelectable> a_selectables)
        {
            foreach (var selectable in a_selectables)
            {
                if (InsideSelectionBox(m_mainCamera.WorldToScreenPoint(selectable.GameObject().transform.position),
                                       m_bounds))
                {
                    m_selected.Add(selectable);
                }
                else
                {
                    m_selected.Remove(selectable);
                }
            }

            return m_selected;
        }


        private bool InsideSelectionBox(Vector3 a_selectionPosition, Bounds a_bounds)
        {
            return a_selectionPosition.x > a_bounds.min.x &&
                   a_selectionPosition.x < a_bounds.max.x &&
                   a_selectionPosition.y > a_bounds.min.y &&
                   a_selectionPosition.y < a_bounds.max.y;
        }


        public void DisableSelectionBox()
        {
            m_selectionBox.sizeDelta = Vector2.zero;
        }
    }
}

