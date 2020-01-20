using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static HorizontalLayoutGroup layoutGroup;
    public static Camera cam;
    public static Transform player;
    public static Vector2 offset;
    public static TouchManager touchManager;
    public static RectTransform rectTransform;

    public GameObject item;
    private GameObject tempItem;
    private Vector2 mousePos;
    public IExecutableObject executableObject;

    // Start is called before the first frame update
    void Start()
    {
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
            if (executableObject.Execute(transform.name))
            {
                Destroy(tempItem.gameObject);
                Destroy(this.gameObject);
                RefreshLayout();
                return;
            }
        }

        tempItem.transform.position = (Vector2)player.position + offset;

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
