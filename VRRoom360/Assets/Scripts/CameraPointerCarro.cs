using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointerCarro : MonoBehaviour
{
    // Start is called before the first frame update
    private const float _maxDistance = 100;
    private GameObject _gazedAtObject = null;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                if (hit.transform.gameObject.name == "Marcha")
                { 
                //_gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
                 _gazedAtObject = hit.transform.gameObject;
                 _gazedAtObject.SendMessage("SonidoPalanca", null, SendMessageOptions.DontRequireReceiver);
                }
                if (hit.transform.gameObject.name == "Menu")
                {
                    //_gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
                    _gazedAtObject = hit.transform.gameObject;
                    _gazedAtObject.SendMessage("RegresarMenu", null, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }

    }
}
