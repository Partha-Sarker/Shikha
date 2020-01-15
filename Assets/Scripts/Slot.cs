using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private HorizontalLayoutGroup layoutGroup;
    public GameObject item;
    private GameObject tempItem;
    private Camera cam;
    private Vector2 mousePos;
    private TouchManager touchManager;
    private RectTransform rectTransform;
    public IExecutableObject executableObject;

    // Start is called before the first frame update
    void Start()
    {
        layoutGroup = GetComponentInParent<HorizontalLayoutGroup>();
        rectTransform = layoutGroup.GetComponent<RectTransform>();
        cam = Camera.main;
        touchManager = FindObjectOfType<TouchManager>();
        RefreshLayout();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(touchManager != null)
            touchManager.enabled = false;
        tempItem = Instantiate(item, Input.mousePosition, Quaternion.identity);
        tempItem.name = transform.name;
        tempItem.GetComponent<SpriteRenderer>().sprite = GetComponent<Image>().sprite;
        tempItem.GetComponent<Pickable>().tempSlot = this;
    }

    public void OnDrag(PointerEventData eventData)
    {
        tempItem.transform.position = (Vector2) cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mousePos = Input.mousePosition;
        if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePos))
        {
            Destroy(tempItem.gameObject);
            RefreshLayout();
            return;
        }

        if(executableObject != null)
        {
            executableObject.Execute(transform.name);
            Destroy(tempItem.gameObject);
            RefreshLayout();
            return;
        }

        Destroy(this.gameObject);
        if(touchManager != null)
            touchManager.enabled = true;
        RefreshLayout();
    }

    private void RefreshLayout()
    {
        layoutGroup.spacing = 1;
        layoutGroup.spacing = 0;
    }
}
