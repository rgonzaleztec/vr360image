using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointerManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject pointer;
    [SerializeField] float maxDistancePointer = 2.5f;
    [Range(0, 1)]
    [SerializeField] float disPointerObject = 0.95f;



    private const float _maxDistance = 20;
    private GameObject _gazedAtObject = null;

    private readonly string interactableTag = "Interactable";
    private float scaleSize = 0.025f;


    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection;
    }

    private void GazeSelection()
    {
        if (_gazedAtObject.name == "Patio")
            _gazedAtObject?.SendMessage("CargarPatio", null, SendMessageOptions.DontRequireReceiver);
        if (_gazedAtObject.name == "EscenaVR360")
            _gazedAtObject?.SendMessage("CargarVRROOM", null, SendMessageOptions.DontRequireReceiver);
        if (_gazedAtObject.name == "Sonido")
            _gazedAtObject?.SendMessage("EncenderAudio", null, SendMessageOptions.DontRequireReceiver);
        if (_gazedAtObject.name == "AcercaDe")
            _gazedAtObject?.SendMessage("CargarAcercaDe", null, SendMessageOptions.DontRequireReceiver);
        if (_gazedAtObject.name == "Fin")
            _gazedAtObject?.SendMessage("SalirApp", null, SendMessageOptions.DontRequireReceiver);
    }

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
                GazeManager.Instance.StartGazeSelection();
                _gazedAtObject = hit.transform.gameObject;
            }
            if (hit.transform.CompareTag(interactableTag))
            {
                PointerOnGaze(hit.point);
            }
            else
            {
                PointerOutGaze();
            }

        }
        else
        {
            // No GameObject detected in front of the camera.
           // _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }

     
    }

    private void PointerOnGaze(Vector3 hitPoint)
    {
        float scalefactor = scaleSize * Vector3.Distance(transform.position, hitPoint);
        pointer.transform.localScale = Vector3.one * scalefactor;
        pointer.transform.parent.position = CalculatePointerPosition(transform.position, hitPoint, disPointerObject);
    }

    private void PointerOutGaze()
    {
        pointer.transform.localScale = Vector3.one * 0.1f;
        pointer.transform.parent.transform.localPosition = new Vector3(0, 0, maxDistancePointer);
        pointer.transform.parent.parent.transform.rotation = transform.rotation;
        GazeManager.Instance.CancelGazeSelection();
    }

    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t)
    {
        float x = p0.x + t * (p1.x - p0.x);
        float y = p0.y + t * (p1.y - p0.y);
        float z = p0.z + t * (p1.z - p0.z);

        return new Vector3(x, y, z);
    }
}
