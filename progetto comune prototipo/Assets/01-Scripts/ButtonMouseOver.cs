using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonMouseOver : MonoBehaviour, IPointerEnterHandler
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

}
