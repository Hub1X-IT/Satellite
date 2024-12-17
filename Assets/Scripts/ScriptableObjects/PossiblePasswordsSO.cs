using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "PossiblePasswordsSO")]
public class PossiblePasswordsSO : ScriptableObject
{
    [SerializeField]
    private string[] possiblePasswords;

    private List<string> possiblePasswordsList;

    public void InitializePossiblePasswords()
    {
        possiblePasswordsList = possiblePasswords.ToList();
    }

    public string GetRandomPassword()
    {
        int length = possiblePasswordsList.Count;
        int randomIndex = Random.Range(0, length);
        string password = possiblePasswordsList[randomIndex];
        possiblePasswordsList.RemoveAt(randomIndex);

        if (possiblePasswordsList.Count == 0)
        {
            Debug.LogWarning("No more possible passwords available! Resetting list.");
            InitializePossiblePasswords();
        }

        return password;
    }    
}