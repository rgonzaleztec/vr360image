using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saltoNavegacion : MonoBehaviour
{
    [SerializeField] GameObject _MiPlayer;

    private Renderer _myRenderer;
    private Vector3 _startingPosition;

    public void Start()
    {
        _startingPosition = transform.localPosition;
        _myRenderer = GetComponent<Renderer>();
    }

    public void SaltoSimple()
    {
        _MiPlayer.transform.position = _startingPosition;
    }

}
