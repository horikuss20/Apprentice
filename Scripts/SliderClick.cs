using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderClick : MonoBehaviour
{
    private AudioSource sliderSource;
    private Slider slider;
    public AudioClip sliderClip;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        sliderSource = slider.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 temp = new Vector3(9f, 5f);
            sliderSource.PlayOneShot(sliderClip, 1f);
        }
    }


}
