using UnityEngine;

public class DetectionManager : MonoBehaviour
{
    private int detectionLevel;
    public int detectionChance;
    public int detectionChanceDefault = 98;
    //Numbers are reversed to make random range work properly for chances
    public bool detected = false;

    public GameObject currentServer;

    private Desk desk;
    private Server server;

    private void Awake()
    {
        desk = FindAnyObjectByType<Desk>();
        server = FindAnyObjectByType<Server>();
    }
    private void OnEnable()
    {
        detectionChance = detectionChanceDefault;
    }
    public void CheckDetection()
    {
        if(Random.Range(1, detectionChance+1) == detectionChance)
        {
            detected = true;
            Destroy(currentServer);
            currentServer = null;
            desk.ShouldEnableDeskTrigger = false;
            desk.ToggleDeskTrigger();
            server.serverTrigger.gameObject.SetActive(false);
        }
        else
        {
            SetDetectionLevel();
            SetDetectionChance();
        }
    }
    private void SetDetectionChance()
    {
        if (detectionLevel == 1)
        {
            detectionChance = detectionChanceDefault;
        }
        else if (detectionLevel == 2)
        {
            detectionChance = 95;
        }
        else if (detectionLevel == 3)
        {
            detectionChance = 90;
        }
        else if (detectionLevel == 4)
        {
            detectionChance = 80;
        }
        else if (detectionLevel == 5)
        {
            detectionChance = 60;
        }
        else if (detectionLevel == 6)
        {
            detectionChance = 30;
        }
        else if (detectionLevel == 7)
        {
            detectionChance = 10;
        }
        else if (detectionLevel == 8)
        {
            detectionChance = 1;
        }
    }
    private void SetDetectionLevel()
    {
        if(detectionLevel < 8)
        {
            detectionLevel++;
        }
        else
        {
            detectionLevel = 8;
        }
    }
}
