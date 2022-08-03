using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float progress;
    [SerializeField] Image loadingFillImg;

    void OnValidate()
    {
        Fill(progress);
    }

    public void Fill(float progress)
    {
        loadingFillImg.fillAmount = Mathf.Lerp(0, 1, progress);
    }
}
