using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    protected static T instance;

    public static T Instance {
        get {
            if (instance == null || instance.Equals(null)) {
                instance = FindObjectOfType<T>();
            }

            return instance;
        }
    }

    public static T GetInstance() {
        if (instance == null || instance.Equals(null))
            instance = FindObjectOfType<T>();
        return instance;
    }

    protected virtual void Awake() {
        if (instance == null)
            instance = this as T;
        else if (instance != this as T)
            DestroyImmediate(gameObject);
    }

    protected virtual void OnDestroy() {
        if (instance == this)
            instance = null;
    }
}