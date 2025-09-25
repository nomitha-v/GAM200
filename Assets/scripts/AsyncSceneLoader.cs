using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AsyncSceneLoader : MonoBehaviour
{
    public void LoadByName(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncRoutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncRoutine(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false; // let us show progress until ready

        // Wait until load reaches 0.9 (Unity loads to 0.9 then waits for activation)
        while (op.progress < 0.9f)
        {
            float progress = op.progress; // 0..0.9
            // Update your UI here (progress / 0.9f to normalize to 0..1)
            Debug.Log($"Loading progress: {progress / 0.9f:P0}");
            yield return null;
        }

        // Optionally do final prep here (fade out UI, wait for player input...)
        op.allowSceneActivation = true; // scene becomes active
        yield return null;
    }
}
