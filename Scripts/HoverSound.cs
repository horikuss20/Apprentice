using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class HoverSound : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource buttonSource;
    public AudioClip buttonAudio;

    void Start()
    {
        buttonSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        buttonSource.PlayOneShot(buttonAudio, .5f);
    }

}