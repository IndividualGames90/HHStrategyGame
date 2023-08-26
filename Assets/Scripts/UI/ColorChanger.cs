using UnityEngine;
using UnityEngine.UI;

namespace IndividualGames.HappyHourStrategyCase
{
    /// <summary>
    /// Cycles through all colors given an image component.
    /// </summary>
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] private Image m_image;

        private float m_h;
        private float m_s;
        private float m_v;
        protected float m_a;

        private Color m_newColor;
        private float m_lerpValue = .3f;


        private void Awake()
        {
            m_a = m_image.color.a;
        }


        private void Update()
        {
            m_image.color = CycleColor(m_image.color);
        }


        protected virtual Color CycleColor(Color a_originalColor)
        {
            Color.RGBToHSV(a_originalColor, out m_h, out m_s, out m_v);

            m_h += m_lerpValue * Time.deltaTime;
            m_h = Mathf.Repeat(m_h, 1);

            m_newColor = Color.HSVToRGB(m_h, m_s, m_v);
            m_newColor.a = m_a;

            return m_newColor;
        }
    }
}