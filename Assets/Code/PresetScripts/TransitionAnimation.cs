using UnityEngine;
using System.Collections;

public class TransitionAnimation : MonoBehaviour
{
    [SerializeField] RectTransform _orangeSquareRT;
    [SerializeField] float _startYScale = 0;
    [SerializeField] float _endYScale = 1;

    
    float _outDuration = 0.5f;
    public IEnumerator OutAnimation()
    {
        Singleton.Instance.Game.SetActiveAllInput(false);
        _orangeSquareRT.pivot = new Vector2(0.5f, 1f);
        float t = 0;
        while(t <= 1)
        {
            t += Time.unscaledDeltaTime/_outDuration;
            float newY = Mathf.Lerp(_startYScale, _endYScale, Ease.OutQuart(t));
            _orangeSquareRT.localScale = new Vector3(1, newY, 1);
            yield return null;
        }
        _orangeSquareRT.localScale = new Vector3(1, Mathf.Lerp(_startYScale, _endYScale, Ease.OutQuart(1)), 1);
    }

    float _inDuration = 0.5f;
    public IEnumerator InAnimation()
    {
        _orangeSquareRT.pivot = new Vector2(0.5f, 0f);
        float t = 0;
        while(t <= 1)
        {
            t += Time.unscaledDeltaTime/_outDuration;
            float newY = Mathf.Lerp(_endYScale, _startYScale, Ease.OutQuart(t));
            _orangeSquareRT.localScale = new Vector3(1, newY, 1);
            yield return null;
        }
        _orangeSquareRT.localScale = new Vector3(1, Mathf.Lerp(_endYScale, _startYScale, Ease.OutQuart(1)), 1);
        Singleton.Instance.Game.SetActiveAllInput(true);
    }
}
