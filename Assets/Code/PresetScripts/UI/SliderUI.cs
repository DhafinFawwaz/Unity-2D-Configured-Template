using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;



#if UNITY_EDITOR
using UnityEditor;
#endif

[AddComponentMenu("UI/SliderUI")]
public class SliderUI : Slider
{
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }
    float ClampValue(float input)
    {
        float newValue = Mathf.Clamp(input, minValue, maxValue);
        if (wholeNumbers)
            newValue = Mathf.Round(newValue);
        return newValue;
    }
    public override void SetValueWithoutNotify(float input)
    {
        float newValue = ClampValue(input);
        if (m_Value == newValue) return;
        m_Value = newValue;
        UpdateVisualsImmediete();
    }

    protected override void Set(float input, bool sendCallback = true)
    {
        float newValue = ClampValue(input);
        if (m_Value == newValue) return;

        m_Value = newValue;
        UpdateVisuals();
        if (sendCallback)
        {
            UISystemProfilerApi.AddMarker("Slider.value", this);
            onValueChanged.Invoke(newValue);
        }
        
    }


    // Everything below this is copy-pasted from the original Slider.cs
    // It has to be done because for some reason, Unity decided to make everything private when
    // it could have been protected, and I need to access those variables to make the slider work
    // So many things is remade
    // also there will be variable with prefix _m_. It also has to be done because unity does not
    // allow to use the same name for variable and property if they are in a subclass even if they
    // are private
    private DrivenRectTransformTracker m_Tracker;
    private enum Axis
    {
        Horizontal = 0,
        Vertical = 1
    }
    private Direction m_Direction { get => direction; set => direction = value;}
    private Axis axis { get { return (m_Direction == Direction.LeftToRight || m_Direction == Direction.RightToLeft) ? Axis.Horizontal : Axis.Vertical; } }
    private bool reverseValue { get { return m_Direction == Direction.RightToLeft || m_Direction == Direction.TopToBottom; } }
    protected override void OnDisable()
    {
        m_Tracker.Clear();
        base.OnDisable();
    }
    
    Image _m_FillImage;
    void UpdateVisuals()
    {
        m_Tracker.Clear();

        _key++;

        if (fillRect != null) // previously (m_FillContainerRect != null)
        {
            m_Tracker.Add(this, _m_FillRect, DrivenTransformProperties.Anchors);
            Vector2 anchorMin = Vector2.zero;
            Vector2 anchorMax = Vector2.one;

            if (_m_FillImage != null && _m_FillImage.type == Image.Type.Filled)
            {
                _m_FillImage.fillAmount = normalizedValue;
            }
            else
            {
                if (reverseValue)
                    anchorMin[(int)axis] = 1 - normalizedValue;
                else
                    anchorMax[(int)axis] = normalizedValue;
            }

            StartCoroutine(TweenAnchor(_m_FillRect, anchorMin, anchorMax, 0.15f));
        }

        if (handleRect != null) // previously (m_HandleContainerRect != null)
        {
            m_Tracker.Add(this, _m_HandleRect, DrivenTransformProperties.Anchors);
            Vector2 anchorMin = Vector2.zero;
            Vector2 anchorMax = Vector2.one;
            anchorMin[(int)axis] = anchorMax[(int)axis] = (reverseValue ? (1 - normalizedValue) : normalizedValue);
            StartCoroutine(TweenAnchor(_m_HandleRect, anchorMin, anchorMax, 0.15f));
        }
    }

    void UpdateVisualsImmediete()
    {
        m_Tracker.Clear();

        _key++;

        if (fillRect != null) // previously (m_FillContainerRect != null)
        {
            m_Tracker.Add(this, _m_FillRect, DrivenTransformProperties.Anchors);
            Vector2 anchorMin = Vector2.zero;
            Vector2 anchorMax = Vector2.one;

            if (_m_FillImage != null && _m_FillImage.type == Image.Type.Filled)
            {
                _m_FillImage.fillAmount = normalizedValue;
            }
            else
            {
                if (reverseValue)
                    anchorMin[(int)axis] = 1 - normalizedValue;
                else
                    anchorMax[(int)axis] = normalizedValue;
            }
            _m_FillRect.anchorMin = anchorMin;
            _m_FillRect.anchorMax = anchorMax;
        }

        if (handleRect != null) // previously (m_HandleContainerRect != null)
        {
            m_Tracker.Add(this, _m_HandleRect, DrivenTransformProperties.Anchors);
            Vector2 anchorMin = Vector2.zero;
            Vector2 anchorMax = Vector2.one;
            anchorMin[(int)axis] = anchorMax[(int)axis] = (reverseValue ? (1 - normalizedValue) : normalizedValue);
            _m_HandleRect.anchorMin = anchorMin;
            _m_HandleRect.anchorMax = anchorMax;
        }
    }

    RectTransform _m_FillRect {get => fillRect; set => fillRect = value;}
    RectTransform _m_HandleRect {get => handleRect; set => handleRect = value;}

    byte _key;
    IEnumerator TweenAnchor(RectTransform rt, Vector2 targetAnchorMin, Vector2 targetAnchorMax, float duration)
    {
        float t = 0;
        byte requirement = _key;
        Vector2 startAnchorMin = rt.anchorMin;
        Vector2 startAnhcorMax = rt.anchorMax;
        while(t <= 1 && requirement == _key)
        {
            rt.anchorMin = Vector2.Lerp(startAnchorMin, targetAnchorMin, Ease.OutQuart(t));
            rt.anchorMax = Vector2.Lerp(startAnhcorMax, targetAnchorMax, Ease.OutQuart(t));
            t += Time.unscaledDeltaTime/duration;
            yield return null;
        }
        if(requirement == _key)
        {
            rt.anchorMin = targetAnchorMin;
            rt.anchorMax = targetAnchorMax;
        }
    }

#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Create SliderUI")]
    static void CreateButtonUI(MenuCommand menuCommand)
    {
        GameObject selected = Selection.activeGameObject;
        GameObject sliderUIGo = new GameObject("SliderUI");
        GameObject backgroundGo = new GameObject("Background");
        GameObject fillAreaGo = new GameObject("Fill Area");
        GameObject fillGo = new GameObject("Fill");
        GameObject handleSlideAreaGo = new GameObject("Handle Slide Area");
        GameObject handleGo = new GameObject("Handle");
        GameObjectUtility.SetParentAndAlign(sliderUIGo, selected);
        GameObjectUtility.SetParentAndAlign(backgroundGo, sliderUIGo);
        GameObjectUtility.SetParentAndAlign(fillAreaGo, sliderUIGo);
            GameObjectUtility.SetParentAndAlign(fillGo, fillAreaGo);
        GameObjectUtility.SetParentAndAlign(handleSlideAreaGo, sliderUIGo);
            GameObjectUtility.SetParentAndAlign(handleGo, handleSlideAreaGo);

        RectTransform sliderUIRect = sliderUIGo.AddComponent<RectTransform>();
        RectTransform backgroundRect = backgroundGo.AddComponent<RectTransform>();
        RectTransform fillAreaRect = fillAreaGo.AddComponent<RectTransform>();
            RectTransform fillRect = fillGo.AddComponent<RectTransform>();
        RectTransform handleSlideAreaRect = handleSlideAreaGo.AddComponent<RectTransform>();
            RectTransform handleRect = handleGo.AddComponent<RectTransform>();

        sliderUIRect.sizeDelta = new Vector2(800, 50);
        sliderUIRect.anchoredPosition = Vector2.zero;
        sliderUIRect.localScale = Vector3.one;

        backgroundRect.anchorMin = new Vector2(0, 0.15f);
        backgroundRect.anchorMax = new Vector2(1, 0.85f);
        backgroundRect.sizeDelta = Vector2.zero;
        backgroundRect.SetLeft(0);
        backgroundRect.SetRight(0);
        backgroundRect.pivot = new Vector2(0.5f, 0.5f);

        fillAreaRect.anchorMin = new Vector2(0, 0.15f);
        fillAreaRect.anchorMax = new Vector2(1, 0.85f);
        fillAreaRect.sizeDelta = Vector2.zero;
        fillAreaRect.SetLeft(0);
        fillAreaRect.SetRight(0);
        fillAreaRect.pivot = new Vector2(0.5f, 0.5f);

        fillRect.anchorMin = new Vector2(0, 0);
        fillRect.anchorMax = new Vector2(0, 1);
        fillRect.pivot = new Vector2(0.5f, 0.5f);
        fillRect.sizeDelta = new Vector2(10, 0);
        fillRect.SetTop(7);
        fillRect.SetBottom(7);
        fillRect.SetLeft(7);
        fillRect.SetRight(7);

        handleSlideAreaRect.anchorMin = new Vector2(0, 0);
        handleSlideAreaRect.anchorMax = new Vector2(1, 1);
        handleSlideAreaRect.SetLeft(25);
        handleSlideAreaRect.SetRight(25);
        handleSlideAreaRect.SetTop(0);
        handleSlideAreaRect.SetBottom(0);
        handleSlideAreaRect.pivot = new Vector2(0.5f, 0.5f);

        handleRect.anchorMin = new Vector2(0, 0);
        handleRect.anchorMax = new Vector2(0, 1);
        handleRect.pivot = new Vector2(0.5f, 0.5f);
        handleRect.sizeDelta = new Vector2(50, 0);
        handleRect.SetTop(0);
        handleRect.SetBottom(0);
            

        Image backgroundImg = backgroundGo.AddComponent<Image>();
        // backgroundImg.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
        backgroundImg.type = Image.Type.Sliced;
        backgroundImg.color = new Color32(0x30, 0x30, 0x30, 0xff);

        Image fillImg = fillGo.AddComponent<Image>();
        // fillImg.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        fillImg.type = Image.Type.Sliced;
        fillImg.color = new Color32(0x42, 0xbc, 0xff, 0xff);

        Image handleImg = handleGo.AddComponent<Image>();
        // handleImg.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
        handleImg.color = new Color32(255, 255, 255, 255);

        SliderUI sliderUI = sliderUIGo.AddComponent<SliderUI>();
        sliderUI.fillRect = fillRect;
        sliderUI.handleRect = handleRect;
        sliderUI.wholeNumbers = true;
        sliderUI.minValue = 0;
        sliderUI.maxValue = 5;
        sliderUI.transition = Selectable.Transition.None;
        
    }
#endif

}
