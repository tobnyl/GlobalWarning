using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class AudioManager : MonoBehaviour
{

    public AudioClip playerShoot;
    public AudioClip enemyDied;
    public AudioClip enemyShoot1;
    public AudioClip enemyShoot2;
    public AudioClip enemyShoot3;
    public AudioClip shieldDown;
    public AudioClip shieldUp;
    public AudioClip shieldBlock;
    public AudioClip playerHit;

    public AudioSource AudioPrefab;

    private List<AudioClip> enemyShoots;

    public static AudioManager _instance;
    public static AudioManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = GetComponent<AudioManager>();
        }
    }

    // Use this for initialization
    void Start()
    {
        enemyShoots = new List<AudioClip>();
        enemyShoots.Add(enemyShoot1);
        enemyShoots.Add(enemyShoot2);
        enemyShoots.Add(enemyShoot3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerShoot()
    {
        InstantiateAudioSourceAndPlay(playerShoot);
    }

    public void EnemyShoot()
    {
        var randomNumber = Random.Range(0, 3);
        InstantiateAudioSourceAndPlay(enemyShoots[randomNumber]);
    }

    public void EnemyDies()
    {
        InstantiateAudioSourceAndPlay(enemyDied);
    }

    public void ShieldDown()
    {
        InstantiateAudioSourceAndPlay(shieldDown);
    }

    public void ShieldUp()
    {
        InstantiateAudioSourceAndPlay(shieldUp);
    }

    public void ShieldBlock()
    {
        InstantiateAudioSourceAndPlay(shieldBlock);
    }

    public void PlayerHit()
    {
        InstantiateAudioSourceAndPlay(playerHit);
    }

    private void InstantiateAudioSourceAndPlay(AudioClip clip)
    {
        AudioSource audioSource = Instantiate(AudioPrefab, transform.position, transform.rotation) as AudioSource;
        Headache audioSourceObject = audioSource.GetComponent<Headache>();

        audioSource.clip = clip;
        audioSource.Play();
    }
}
