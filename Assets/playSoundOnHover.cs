using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class playSoundOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioClip Audio;
    private AudioSource AudioSource;
    private void Start()
    {
        AudioSource = this.GetComponent<AudioSource>();
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        AudioSource.PlayOneShot(Audio, 1);
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        AudioSource.Stop();
    }


}
