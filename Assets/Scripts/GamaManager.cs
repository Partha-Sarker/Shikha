using UnityEngine;
using UnityEngine.UI;

public class GamaManager : MonoBehaviour
{
    private Vector2 playerInitialPosition;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private HorizontalLayoutGroup layoutGroup;
    [SerializeField]
    private Vector2 offset;
    [SerializeField]
    private TouchManager touchManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        playerInitialPosition = player.transform.position;
        SetupSlot();
        SetupPickable();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            player.position = playerInitialPosition;
    }

    private void SetupSlot()
    {
        Slot.cam = Camera.main;
        Slot.layoutGroup = layoutGroup;
        Slot.offset = offset;
        Slot.player = player;
        Slot.touchManager = touchManager;
        Slot.rectTransform = layoutGroup.GetComponent<RectTransform>();
    }

    private void SetupPickable()
    {
        Pickable.touchManager = touchManager;
        Pickable.slotUI = layoutGroup.transform;
    }

}
