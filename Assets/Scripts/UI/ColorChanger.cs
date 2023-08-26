using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Image m_image;

    private float m_h;
    private float m_s;
    private float m_v;
    private float m_a;

    private Color m_newColor;
    private float m_lerpValue = .3f;


    private void Awake()
    {
        m_a = m_image.color.a;
    }


    private void Update()
    {
        CycleColor();
    }


    private void CycleColor()
    {
        Color.RGBToHSV(m_image.color, out m_h, out m_s, out m_v);

        m_h += m_lerpValue * Time.deltaTime;
        m_h = Mathf.Repeat(m_h, 1);

        m_newColor = Color.HSVToRGB(m_h, m_s, m_v);
        m_newColor.a = m_a;

        m_image.color = m_newColor;
    }
}
