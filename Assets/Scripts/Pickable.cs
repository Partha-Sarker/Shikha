using UnityEngine.UI;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public static TouchManager touchManager;
    public static Transform slotUI;

    private string name;
    private Sprite sprite;
    public GameObject slot;
    [HideInInspector]
    public Slot tempSlot;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        name = transform.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IExecutableObject>() != null)
        {
            if (tempSlot == null)
                return;
            tempSlot.executableObject = collision.GetComponent<IExecutableObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IExecutableObject>() != null)
        {
            if (tempSlot == null)
                return;
            tempSlot.executableObject = null;
        }
    }

    private void OnMouseDown()
    {
        if(touchManager != null)
            touchManager.enabled = false;
    }

    private void OnMouseUp()
    {
        if (touchManager != null)
            touchManager.enabled = true;
        int childrenCount = slotUI.transform.childCount;
        if (childrenCount > 4)
        {
            print("Can't pick more than 4 items");
            return;
        }
        GameObject slotItem = Instantiate(slot);
        slotItem.transform.parent = slotUI;
        slotItem.name = name;
        slotItem.GetComponent<Image>().sprite = sprite;
        Destroy(this.gameObject);
    }
}
