using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    PushableBox pushableBox;

    // Start is called before the first frame update
    void Start()
    {
        pushableBox = GetComponentInParent<PushableBox>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        //print("Player enters");
        //playerMovement.whatIsGround ^= (boxLayerIndex << 10);
        pushableBox.checkPush = true;
        pushableBox.rb.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        //print("Player exits");
        //playerMovement.whatIsGround |= (boxLayerIndex << 10);
        pushableBox.checkPush = false;
        pushableBox.rb.bodyType = RigidbodyType2D.Dynamic;
        //DisableJoint();
    }
}
