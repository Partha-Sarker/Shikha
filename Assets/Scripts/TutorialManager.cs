using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public Transform player;

    public int walkSpeed = 7;
    public int pushPullSpeed = 4;
    public int crouchSpeed = 4;
    public int jumpForce = 9;

    public float walkAnimationSpeed = 1;
    public float pushPullAnimationSpeed = .75f;
    public float crouchAnimationSpeed = .75f;

    private Rigidbody2D playerRB;
    private Vector2 initialPlayerPosition;
    private Vector2 initialBoxPosition;
    private Animator animator;
    private Animator playerAnimator;
    public Button nextButton;
    public Button previousButton;
    public Button skipButton;
    public TextMeshProUGUI headerText;
    private Vector3 initialScale;
    public GameObject box;


    private void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        initialPlayerPosition = player.position;
        initialBoxPosition = box.transform.position;
        playerAnimator = player.GetComponent<Animator>();
        animator = GetComponent<Animator>();
        initialScale = player.localScale;
    }

    public void WalkForward()
    {
        playerRB.velocity = new Vector2(walkSpeed, 0);
        playerAnimator.SetFloat("HSpeed", walkAnimationSpeed);
        playerAnimator.SetBool("isWalking", true);
    }

    public void WalkBackward()
    {
        Vector3 scaler = player.localScale;
        scaler.x *= -1;
        player.localScale = scaler;

        playerRB.velocity = new Vector2(-walkSpeed, 0);
        playerAnimator.SetFloat("HSpeed", walkAnimationSpeed);
        playerAnimator.SetBool("isWalking", true);
    }

    public void Push()
    {
        playerRB.velocity = new Vector2(pushPullSpeed, 0);
        playerAnimator.SetFloat("HSpeed", pushPullAnimationSpeed);
        playerAnimator.SetBool("isWalking", true);
    }

    public void Pull()
    {
        playerRB.velocity = new Vector2(-pushPullSpeed, 0);
        playerAnimator.SetFloat("HSpeed", -pushPullAnimationSpeed);
        playerAnimator.SetBool("isWalking", true);
    }

    public void CrouchForward()
    {
        playerRB.velocity = new Vector2(crouchSpeed, 0);
        playerAnimator.SetFloat("HSpeed", crouchAnimationSpeed);
        playerAnimator.SetBool("isWalking", true);
    }

    public void CrouchBackward()
    {
        Vector3 scaler = player.localScale;
        scaler.x *= -1;
        player.localScale = scaler;

        playerRB.velocity = new Vector2(-crouchSpeed, 0);
        playerAnimator.SetFloat("HSpeed", crouchAnimationSpeed);
        playerAnimator.SetBool("isWalking", true);
    }

    public void StopMovement()
    {
        playerRB.velocity = new Vector2(0, 0);
        playerAnimator.SetFloat("HSpeed", 0);
        playerAnimator.SetBool("isWalking", false);
    }

    public void Jump()
    {
        playerRB.velocity = Vector2.up * jumpForce;
        playerAnimator.SetBool("isJumping", true);

    }

    public void HoldBox()
    {
        DistanceJoint2D boxJoint = box.GetComponent<DistanceJoint2D>();
        boxJoint.enabled = true;
        Rigidbody2D boxRb = box.GetComponent<Rigidbody2D>();
        boxRb.bodyType = RigidbodyType2D.Dynamic;
        playerAnimator.SetBool("isPushing", true);
    }

    public void Crouch()
    {
        playerAnimator.SetBool("isCrouching", true);
    }

    public void StandUp()
    {
        playerAnimator.SetBool("isCrouching", false);
    }

    public void ResetPlayer()
    {
        playerRB.velocity = new Vector2(0, 0);
        playerAnimator.SetFloat("HSpeed", 0);
        playerAnimator.SetBool("isWalking", false);
        playerAnimator.SetBool("isJumping", false);
        playerAnimator.SetBool("isPushing", false);
        playerAnimator.SetBool("isCrouching", false);

        box.transform.position = initialBoxPosition;
        player.position = initialPlayerPosition;
        player.localScale = initialScale;
    }

    public void OnMoveTutorialEnd()
    {
        if (!nextButton.IsInteractable())
        {
            EnableNextButton();
            animator.SetTrigger("Active Next");
        }

        ResetPlayer();

    }

    public void OnJumpTutorialEnd()
    {
        if (!previousButton.IsInteractable())
        {
            EnablePreviousButton();
            EnableNextButton();
            animator.SetTrigger("Active Previous");
        }

        ResetPlayer();
    }

    public void OnPushPullTutorialEnd()
    {
        if (!previousButton.IsInteractable())
        {
            EnablePreviousButton();
            EnableNextButton();
        }

        ResetPlayer();
    }

    public void OnCrouchTutorialEnd()
    {
        if (!previousButton.IsInteractable())
            EnablePreviousButton();
        if (!skipButton.IsInteractable())
        {
            EnableSkipButton();
            animator.SetTrigger("Active Skip");
        }

        ResetPlayer();
    }

    public void StopJumping()
    {
        playerAnimator.SetBool("isJumping", false);
    }

    public void ChangeHeaderText(string text)
    {
        headerText.SetText(text);
    }

    public void OnNextButtonClicked()
    {
        animator.SetTrigger("Next");
        ResetPlayer();
    }

    public void OnPreviousClicked()
    {
        animator.SetTrigger("Previous");
        ResetPlayer();
    }

    public void OnSkipButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void DisablePreviousButton()
    {
        previousButton.interactable = false;
    }

    public void EnablePreviousButton()
    {
        previousButton.interactable = true;
    }

    public void EnableNextButton()
    {
        nextButton.interactable = true;
    }

    public void DisableNextButton()
    {
        nextButton.interactable = false;
    }

    public void EnableSkipButton()
    {
        skipButton.interactable = true;
    }

    public void ActiveBox()
    {
        box.SetActive(true);
    }

    public void DisableBox()
    {
        box.SetActive(false);
    }

}
