using UnityEngine;

public class App : MonoBehaviour
{
    public static App Instance;

    public static bool debugging = false;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
