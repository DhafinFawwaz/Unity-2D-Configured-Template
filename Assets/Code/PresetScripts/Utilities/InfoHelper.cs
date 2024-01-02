using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class InfoHelper : MonoBehaviour
{
    [SerializeField] bool _showDebug = true;
    [SerializeField] Color _color = Color.red;
    [SerializeField] int _fontSize = 30;
    [SerializeField] int _offset = 20;
	[SerializeField] Rect _fpsRect = new Rect(20, 20, 400, 100);
 	GUIStyle _style;
	float _fps;
    void Start()
    {
        
        _style = new GUIStyle();
        _style.fontSize = _fontSize;
        _style.normal.textColor = _color;
        StartCoroutine(RecalculateFPS());
    }

	private IEnumerator RecalculateFPS()
	{
		while (true)
		{
            _style.fontSize = _fontSize;
			_fps = 1/Time.deltaTime;
			yield return new WaitForSeconds(1);
		}
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))_showDebug = !_showDebug;
    }
    void OnGUI()
    {
        if(!_showDebug)return;
        _style.normal.textColor = _color;

        float multiplier = (float)Screen.height/1080;
        _style.fontSize = (int)(_fontSize * multiplier);

        _fpsRect.x = _offset;
        _fpsRect.y = _offset;
        Rect labelRect = new Rect(_fpsRect.x * multiplier, 
                           _fpsRect.y * multiplier, 
                           _fpsRect.width * multiplier, 
                           _fpsRect.height * multiplier);
        GUI.Label(labelRect,"<b>FPS: "+ string.Format("{0:0.0}\n"+
            "MaxRes: " +Display.main.systemWidth.ToString()+"x"+Display.main.systemHeight.ToString() + "\n" +
            "Res: "  +Screen.width.ToString()+"x"+Screen.height.ToString() + "</b>"
            
        ,_fps),_style);
    }
}
