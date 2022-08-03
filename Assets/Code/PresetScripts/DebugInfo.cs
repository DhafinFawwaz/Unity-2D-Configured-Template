using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class DebugInfo : MonoBehaviour
{
    [SerializeField]bool showDebug = true;
    [SerializeField] Color color = Color.red;
    [SerializeField] int fontSize = 30;
    [SerializeField] int offset = 20;
	[SerializeField] Rect fpsRect = new Rect(20, 20, 400, 100);
 	GUIStyle style;
	float fps;
    void Start()
    {
        
        style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = color;
        StartCoroutine(RecalculateFPS());
    }

	private IEnumerator RecalculateFPS()
	{
		while (true)
		{
            style.fontSize = fontSize;
			fps = 1/Time.deltaTime;
			yield return new WaitForSeconds(1);
		}
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))showDebug = !showDebug;
    }
    void OnGUI()
    {
        if(!showDebug)return;
        style.normal.textColor = color;

        float multiplier = (float)Screen.height/1080;
        style.fontSize = (int)(fontSize * multiplier);

        fpsRect.x = offset;
        fpsRect.y = offset;
        Rect labelRect = new Rect(fpsRect.x * multiplier, 
                           fpsRect.y * multiplier, 
                           fpsRect.width * multiplier, 
                           fpsRect.height * multiplier);
        GUI.Label(labelRect,"<b>FPS: "+ string.Format("{0:0.0}\n"+
            "MaxRes: " +Display.main.systemWidth.ToString()+"x"+Display.main.systemHeight.ToString() + "\n" +
            "Res: "  +Screen.width.ToString()+"x"+Screen.height.ToString() + "</b>"
            
        ,fps),style);
    }
}
