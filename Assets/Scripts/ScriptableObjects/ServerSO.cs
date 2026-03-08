using UnityEngine;

[CreateAssetMenu(menuName = "ServerSO")]
public class ServerSO : ScriptableObject
{
    [SerializeField]
    private string serverID;

    public string ServerID => serverID;

    public int CurrentDetectionChance { get; set; }
}
