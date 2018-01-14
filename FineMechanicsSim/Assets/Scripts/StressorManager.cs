using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressorManager : MonoBehaviour
{
    public AudioClip[] stressors;
    public AudioSource audioSource;

    private float pastTime;
    private float occurence;
    private bool playStressors = false;

    // Use this for initialization
    void Start()
    {
        stressors = Resources.LoadAll<AudioClip>("Stressors");
        audioSource = GetComponent<AudioSource>();
        print("loaded stressors: " + stressors.Length);
    }

    void Update()
    {
        PlayStressor();

    }
    public void StartStressors(float occurence)
    {
        this.occurence = occurence;
        pastTime = Time.time;

        playStressors = true;
    }

    public void StopStressors()
    {
        playStressors = false;
    }
    private void PlayStressor()
    {
        var currentTime = Time.time;
        var delta = currentTime - pastTime;

        //ako je prošla sekunda od zadnje provjere 
        if (playStressors && delta >= 1)
        {
            pastTime = currentTime;

            //pusti stressor ovisno o šansi za stressor
            if (Random.value < occurence)
            {
                SimulationLog.LogStressor(currentTime);
                var stressorIndex = (int)(Random.value * 1000) % stressors.Length;

                audioSource.clip = stressors[stressorIndex];
                audioSource.Play();

            }
        }

    }


}
