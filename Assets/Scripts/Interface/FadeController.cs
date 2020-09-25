using UnityEngine;

public class FadeController : MonoBehaviour {
    private Animator animator;

	void Start () {
        animator = GetComponent<Animator>();
	}

    public void StartFade()
    {
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
    }
}
