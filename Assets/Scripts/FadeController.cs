using UnityEngine;

public class FadeController : MonoBehaviour {
    private Animator _animator;

	void Start () {
        _animator = GetComponent<Animator>();
	}

    public void StartFade()
    {
        _animator.SetBool("canFade", true);
        Invoke("HideFade", 0.5f);
    }

    public void HideFade()
    {
        _animator.SetBool("canFadeOut", true);
        Invoke("SetDefaultState", 0.5f);
    }

    private void SetDefaultState()
    {
        _animator.SetBool("canFade", false);
        _animator.SetBool("canFadeOut", false);
    }
}
