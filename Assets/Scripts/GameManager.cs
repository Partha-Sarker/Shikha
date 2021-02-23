using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isPcInput;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Transform virtualCam;
    [SerializeField]
    private Transform player;
    private Rigidbody2D playerRB;
    private Vector2 playerInitialPosition;
    [SerializeField]
    private HorizontalLayoutGroup layoutGroup;
    [SerializeField]
    private Vector2 itemDropOffset;
    [SerializeField]
    private TouchManager touchManager;
    [SerializeField]
    private SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Awake()
    {
        if (touchManager.gameObject.activeSelf)
            isPcInput = false;
        else
            isPcInput = true;
        playerRB = player.GetComponent<Rigidbody2D>();
        playerInitialPosition = player.transform.position;
        SetupSlot();
        SetupPickable();
        Parallax.player = player;
        Parallax.virtualCam = virtualCam;
        Parallax.virtualCameraInitialPos = virtualCam.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //player.position = playerInitialPosition;
            playerRB.velocity = Vector2.zero;
            playerRB.bodyType = RigidbodyType2D.Kinematic;
            sceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex, "You Fell");

        }
        else if (collision.tag == "backgroundImage")
            return;
        else
        {
            Destroy(collision.gameObject);
        }
    }

    private void SetupSlot()
    {
        Slot.cam = Camera.main;
        Slot.layoutGroup = layoutGroup;
        Slot.offset = itemDropOffset;
        Slot.player = player;
        Slot.rectTransform = layoutGroup.GetComponent<RectTransform>();
    }

    private void SetupPickable()
    {
        Pickable.slotUI = layoutGroup.transform;
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
