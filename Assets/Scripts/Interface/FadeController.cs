using UnityEngine;

public class FadeController : MonoBehaviour {
    private Animator animator;

    // Dependancies
    private PlayerController player;

	void Start () {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
	}

    public void StartFade()
    {
        player.can_move = false;

        animator.SetBool("canFade", true);
        Invoke("HideFade", 0.5f);
    }

    public void HideFade()
    {
        animator.SetBool("canFadeOut", true);
        Invoke("SetDefaultState", 0.5f);
    }

    private void SetDefaultState()
    {
        animator.SetBool("canFade", false);
        animator.SetBool("canFadeOut", false);

        player.can_move = true;
    }
}
