using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject player;
    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject level;
    [SerializeField] float deathRestartDuration;

    private AudioSource audioSource;
    [SerializeField] private AudioClip gameOverAudio;
    [SerializeField] private float audioVolume;
    private float timerRestart;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(player.GetComponent<PcHealth>().GetHealth() <= 0)
        {
            deathUI.SetActive(true);
            level.SetActive(false);
            player.SetActive(false);

            audioSource.PlayOneShot(gameOverAudio, audioVolume);

            timerRestart += Time.deltaTime;

            if(timerRestart >= deathRestartDuration)
            {
                SceneManager.LoadScene("MainScene");
            }

        }
    }
}
