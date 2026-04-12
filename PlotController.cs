using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    private Image previewImage;
    [SerializeField]
    private Vector2 pos;

    private bool planted = false;

    public bool Planted
    {
        get
        {
            return planted;
        }
        set
        {
            planted = value;
        }
    }

    public Vector2 Pos
    {
        get
        {
            return pos;
        }
        set
        {
            pos = value;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1);

        Seed seed = SeedPanelController.Instance.SelectedSeed;

        if (seed != null && !planted)
        {
            previewImage.gameObject.SetActive(true);
            previewImage.sprite = SpriteDB.Instance.seedSprites[seed.SeedID];
        }

        AudioManager.Instance.Play(3);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;

        if (previewImage.gameObject.activeInHierarchy)
        {
            previewImage.gameObject.SetActive(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SeedPanelController.Instance.SelectedSeed != null && !planted)
        {
            PlotPanelController.Instance.Plant(this, SeedPanelController.Instance.SelectedSeed, pos);
        }
    }
}
