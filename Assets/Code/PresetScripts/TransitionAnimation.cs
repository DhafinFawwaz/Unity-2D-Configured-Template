using UnityEngine;

public class TransitionAnimation : MonoBehaviour
{
    [SerializeField] RectTransform orangeSquareRT;
    [SerializeField] GameObject buttonBlocker;
    [SerializeField] float startYScale = 0;
    [SerializeField] float endYScale = 1;
    void Start(){InEnd();}
    public void OutStart()
    {
        buttonBlocker.SetActive(true);
        orangeSquareRT.pivot = new Vector2(0.5f, 1f);
        OutAnimation(0);
    }
    public void OutAnimation(float t)
    {
        float newY = Mathf.Lerp(startYScale, endYScale, Ease.OutQuart(t));
        orangeSquareRT.localScale = new Vector3(1, newY, 1);
    }
    public void OutEnd()
    {
        OutAnimation(1);
    }



    public void InStart()
    {
        orangeSquareRT.pivot = new Vector2(0.5f, 0f);
        InAnimation(0);
    }
    public void InAnimation(float t)
    {
        float newY = Mathf.Lerp(endYScale, startYScale, Ease.OutQuart(t));
        orangeSquareRT.localScale = new Vector3(1, newY, 1);
    }
    public void InEnd()
    {
        buttonBlocker.SetActive(false);
        InAnimation(1);
    }
}
