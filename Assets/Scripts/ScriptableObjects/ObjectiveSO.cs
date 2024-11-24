using UnityEngine;

[CreateAssetMenu(menuName = "Objectives/ObjectiveSO")]
public class ObjectiveSO : ScriptableObject
{
    [SerializeField]
    private string objective;

    public string Objective => objective;
}
