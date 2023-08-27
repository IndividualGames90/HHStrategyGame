using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Selected Unit list manager.
    /// </summary>
    public class UnitSelector
    {
        Dictionary<int, GameObject> m_selectedUnits = new();


        public void SelectUnit(GameObject a_unit)
        {
            int id = a_unit.GetInstanceID();
            var ourUnit = a_unit.GetComponent<PhotonView>().IsMine;

            if (!(m_selectedUnits.ContainsKey(id)) && ourUnit)
            {
                m_selectedUnits.Add(id, a_unit);
                a_unit.GetComponent<UnitSelectionCirle>().Selected();
            }
        }


        public void DeselectAll()
        {
            foreach (var item in m_selectedUnits)
            {
                item.Value.gameObject.GetComponentInChildren<UnitSelectionCirle>().Selected();
            }
            m_selectedUnits.Clear();
        }


        public void MoveUnitsTo(GridController a_gridController, NavGridElement a_destinationElement)
        {
            foreach (var unit in m_selectedUnits.Values)
            {
                unit.GetComponent<UnitController>().MoveToDestination(a_gridController, a_destinationElement);
            }
        }
    }
}

