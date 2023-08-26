using TMPro;
using UnityEngine;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Cycles through all colors given TMPro component.
    /// </summary>
    public class ColorChangerTextMeshPro : ColorChanger
    {
        [SerializeField] private TextMeshProUGUI m_label;


        private void Awake()
        {
            m_a = m_label.color.a;
        }


        private void Update()
        {
            m_label.color = CycleColor(m_label.color);
        }


        protected override Color CycleColor(Color a_originalColor)
        {
            return base.CycleColor(a_originalColor);
        }
    }
}