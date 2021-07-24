using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonKeyBinding : MonoBehaviour
{
    public string key;
    public void Update()
    {
        if(Input.GetKeyDown(key))
        {
            gameObject.GetComponent<Button>().onClick.Invoke();
        }
    }
}
