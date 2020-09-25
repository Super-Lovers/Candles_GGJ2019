using UnityEngine;

public class InterfaceController : MonoBehaviour {
    private void Update() {
        if (Input.anyKeyDown) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("game");
        }
    }
}
