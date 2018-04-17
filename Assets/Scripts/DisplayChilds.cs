using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayChilds : MonoBehaviour, IPointerClickHandler
{
    int i = 0;
   public  GameObject panel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (i < this.transform.childCount)
        {
            if (i > 0)
            { this.transform.GetChild(i - 1).gameObject.SetActive(false); }
            Debug.Log("Grave");
            Debug.Log(this.transform.GetChild(i).name);
            this.transform.GetChild(i).gameObject.SetActive(true);
            i++;
        }
    }
}
