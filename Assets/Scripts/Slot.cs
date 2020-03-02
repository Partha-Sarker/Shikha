using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static HorizontalLayoutGroup layoutGroup;
    public static Camera cam;
    public static Transform player;
    public static Vector2 offset;
    public static RectTransform rectTransform;

    private TouchManager touchManager;
    public GameObject item;
    private GameObject tempItem;
    private Vector2 mousePos;
    public IExecutableObject executableObject;

    // Start is called before the first frame update
    void Start()
    {
        if(!GameManager.isPcInput)
            touchManager = GameObject.FindGameObjectWithTag("manager").GetComponent<TouchManager>();
        RefreshLayout();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GameManager.isPcInput)
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
        RefreshLayout();
    }

    private void RefreshLayout()
    {
        if(!GameManager.isPcInput)
            touchManager.enabled = true;
        layoutGroup.spacing = 1;
        layoutGroup.spacing = 0;
    }
}
