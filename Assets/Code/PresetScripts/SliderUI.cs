using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    [SerializeField] int maxSliderValue = 7;
    [SerializeField] float duration = 0.1f;
    [SerializeField] Slider slider;
    [SerializeField] Transform handleImg;
    [SerializeField] Transform handlePos;
    [SerializeField] Image fillImg;


    float previousValue;
    void Start()
    {
        previousValue = slider.value;
    }
    public void SetValueInstant(float newVal)
    {
        sliderValue = newVal;
        fillImg.fillAmount = sliderValue;
        slider.value = sliderValue;
    }
    public void SetValue()
    {
        float newValue = Mathf.Round(slider.value*maxSliderValue)/maxSliderValue;
        if(!(Mathf.Approximately(newValue, previousValue)))
        {
            sliderValueProperty = slider.value;
            StartCoroutine(HandleAnimation(handleImg, handlePos.position, fillImg, newValue));
        }
        slider.value = Mathf.Round(slider.value*maxSliderValue)/maxSliderValue;
        previousValue = slider.value;
    }

    int key;
    IEnumerator HandleAnimation(Transform handle, Vector2 newPos, Image fillImg, float newFill)
    {
        float t = 0;
        Vector2 currentPos = handle.position;
        float currentFill = fillImg.fillAmount;
        key++;
        int requirement = key;
        while(t <= 1 && requirement == key)
        {
            handle.position = Vector2.Lerp(currentPos, newPos, Global.EaseOutQuartCurve(t));
            fillImg.fillAmount = Mathf.Lerp(currentFill, newFill, Global.EaseOutQuartCurve(t));
            t += Time.unscaledDeltaTime/duration;
            yield return null;
        }
        if(requirement == key)
            handle.position = newPos;
    }


    public event OnVariableChangedDelegate OnVariableChanged;
    public delegate void OnVariableChangedDelegate(float newVal);

    float sliderValue;
    [HideInInspector] public float sliderValueProperty
    {
        get
        {
            return sliderValue;
        }
        set
        {
            sliderValue = value;
            if(OnVariableChanged != null)
                OnVariableChanged(sliderValue);
        }
    }

}