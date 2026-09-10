using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicPlayer : MonoBehaviour {
    public AudioSource[] audios;
    static MenuMusicPlayer instance;
    static bool startedGame = false;
    public int b = 0;
    void Start() {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (!startedGame) {
                StartCoroutine(PlayMusicDelayed(1));
                startedGame = true;
            } else {
                StartCoroutine(PlayMusicDelayed(6));
            }
            
        } else {
            Destroy(gameObject);
        }
    }
    IEnumerator PlayMusicDelayed(float delay) {
        for (float i = 0; i < delay; i += Time.deltaTime)
            yield return null;
        
        b = UnityEngine.Random.Range(0, audios.Length);
        audios[b].Play();
    }
    void Update() {
        audios[b].volume = MyPlayerPrefs.instance.GetInt("mutedMusicUI") == 0 ? 0.2f : 0;
    }
}