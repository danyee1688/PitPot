using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeedController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler
{
    [SerializeField]
    private Image image;
    private Seed parentSeed;
    private SeedPanelController seedPanelController; 
    private GameManager gameManager;
    private bool selected = false;

    public bool Selected
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value;

            if (selected)
            {
                seedPanelController.SelectedSeed = parentSeed;
                seedPanelController.SelectedSeedGO = gameObject;

                transform.localScale = new Vector3(1.25f, 1.25f, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
    }

    private const float SEED_MIN_X = -1.25f;
    private const float SEED_MAX_X = -0.95f;
    private const float SEED_MIN_Y = -0.5f;
    private const float SEED_MAX_Y = 0.35f;

    public void Instantiate(Seed seed, SeedPanelController seedPanelController, GameManager gameManager)
    {
        parentSeed = seed;
        this.seedPanelController = seedPanelController;
        this.gameManager = gameManager;

        image.sprite = SpriteDB.Instance.seedSprites[parentSeed.SeedID];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        seedPanelController.UnselectAllSeeds();

        Selected = true;  
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InfoPanelManager.Instance.HideAllInfo();
        InfoPanelManager.Instance.ShowSeedInfo(parentSeed);

        AudioManager.Instance.Play(4);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoPanelManager.Instance.HideSeedInfo();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 targetPos = gameManager.GetMouseWorldPosition();
        Vector2 clampedPos = new Vector2(Mathf.Clamp(targetPos.x, SEED_MIN_X, SEED_MAX_X), Mathf.Clamp(targetPos.y, SEED_MIN_Y, SEED_MAX_Y));

        transform.position = clampedPos;
    }
}
