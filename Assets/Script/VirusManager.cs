
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VirusManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPos;
    [SerializeField] private GameObject virusPrefab;
    [SerializeField] private VirusData virusDatabase;
    float elapsedTime;
    float coolDownTime = 5f;
    float coolDownLoad;
    int waveVirusMinutes = 1;
    int waveVirusSeconds = 10;
    void Start()
    {
        
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        coolDownLoad += Time.deltaTime;
        if (waveVirusSeconds > 60) waveVirusSeconds = 0;
        if (seconds == waveVirusSeconds)
        {
            waveVirusSeconds += 10;
            if (coolDownTime > 0.5f)
            {
                coolDownTime -= 0.2f;
            }
        }
        if (coolDownLoad >= coolDownTime )
        {
            if(minutes == waveVirusMinutes)
            {
                SpawnAllVirus();
                waveVirusMinutes++;
                coolDownLoad = 0;
            }
            else
            {
                SpawnSingleVirus();
                coolDownLoad = 0;
            }
        }
    }

    private void SpawnSingleVirus()
    {
        int index = Random.Range(0, spawnPos.Count);
        int virusIndex = Random.Range(0, virusDatabase.virusData.Count - 1);
        PhotonNetwork.Instantiate(virusDatabase.virusData[virusIndex].virusPrefab.name, spawnPos[index].position, Quaternion.identity);
    }

    private void SpawnAllVirus()
    {
        for(int i = 0; i< spawnPos.Count; i++)
        {
            int index = Random.Range(0, virusDatabase.virusData.Count);
            PhotonNetwork.Instantiate(virusDatabase.virusData[index].virusPrefab.name, spawnPos[i].position, Quaternion.identity);
        }
    }
}
