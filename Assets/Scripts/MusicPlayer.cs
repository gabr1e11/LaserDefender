using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;

    public AudioClip m_startClip;
    public AudioClip m_gameClip;
    public AudioClip m_loseClip;
    public AudioClip m_logoClip;

    private AudioSource m_audioSource;

    // Use this for initialization
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        m_audioSource = GetComponent<AudioSource>();

        m_audioSource.loop = true;

        PlayMenu();

        GameObject.DontDestroyOnLoad(gameObject);

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        m_audioSource.Stop();

        switch (scene.buildIndex)
        {
            case 0:
                PlayMenu();
                break;
            case 1:
                PlayGame();
                break;
            case 2:
            case 3:
                PlayLose();
                break;
        }
        m_audioSource.Play();
    }

    void PlayMenu()
    {
        m_audioSource.clip = m_startClip;
        m_audioSource.volume = 0.6f;
        m_audioSource.Play();
        StartCoroutine(PlayLogo());
    }

    void PlayGame()
    {
        m_audioSource.clip = m_gameClip;
        m_audioSource.volume = 0.4f;
        m_audioSource.Play();
    }

    void PlayLose()
    {
        m_audioSource.clip = m_loseClip;
        m_audioSource.volume = 0.4f;
        m_audioSource.Play();
    }

    IEnumerator PlayLogo()
    {
        yield return new WaitForSeconds(1.0f);

        AudioSource.PlayClipAtPoint(m_logoClip, Camera.main.transform.position);
        yield return null;
    }
}
