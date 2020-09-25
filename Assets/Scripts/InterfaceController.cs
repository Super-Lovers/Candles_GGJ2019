using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour {

	public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
