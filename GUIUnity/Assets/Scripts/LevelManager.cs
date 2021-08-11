using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public Animator mTransition;
    public float mTransitionTime = 1f;

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) {
        mTransition.SetTrigger("Start");

        yield return new WaitForSeconds(mTransitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
