using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }
    public void OnSliderValueChanged(float value)
    {
        text.text = value.ToString("Tree Coverage = " + "0%");
    }
}