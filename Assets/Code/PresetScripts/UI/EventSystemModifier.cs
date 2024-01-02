using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventSystem))]
public class EventSystemModifier : MonoBehaviour
{
    void OnEnable()
    {
        ButtonUI.s_onPointerEnter += OnButtonUIHover;
    }
    void OnDisable()
    {
        ButtonUI.s_onPointerEnter -= OnButtonUIHover;
    }
    void OnButtonUIHover()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        var pointerEventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition};
        EventSystem.current.RaycastAll(pointerEventData, results);
        foreach (var r in results)
        {
            if (r.gameObject && r.gameObject.TryGetComponent(out Selectable sel))
            {
                if (sel.interactable) 
                {
                    EventSystem.current.SetSelectedGameObject(r.gameObject);
                    break;
                }
            }
            // case parent is selectable
            else if (r.gameObject && r.gameObject.transform.parent && r.gameObject.transform.parent.TryGetComponent(out Selectable sel2))
            {
                if (sel2.interactable) 
                {
                    EventSystem.current.SetSelectedGameObject(r.gameObject.transform.parent.gameObject);
                    break;
                }
            }
        }
    }
}
