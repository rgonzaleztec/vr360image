using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class carroInterior : MonoBehaviour
{
    [SerializeField] AudioSource sonidoPalanca;
    private Renderer _myRenderer;
    private Vector3 _startingPosition;

    public void Start()
    {
        _startingPosition = transform.parent.localPosition;
        _myRenderer = GetComponent<Renderer>();
    }


    public void SonidoPalanca()
    {
        if (sonidoPalanca.isPlaying)
            sonidoPalanca.Stop();
        else
            sonidoPalanca.Play();
    }

    public void RegresarMenu()
    {
        SceneManager.LoadScene(0);
    }



}
