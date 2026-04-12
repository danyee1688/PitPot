using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.Play(1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayClickSound();
    }

    private void PlayClickSound()
    {
        AudioManager.Instance.Play(2);
    }
}
