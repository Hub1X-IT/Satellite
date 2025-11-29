using System.Collections;
using DialogSystem.Runtime.Core;
using DialogSystem.Runtime.Models;
using DialogSystem.Runtime.Models.Nodes;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Script for testing whatever is needed
    [SerializeField]
    private DialogManager dialogManager;
    [SerializeField]
    private DialogGraph dialogGraph;
    [SerializeField]
    private ActionNode actionNode;
    [SerializeField]
    private DialogActionRunner dialogActionRunner;

    [SerializeField]
    private GameEventSO gameEventToRaise;

    [SerializeField]
    private bool shouldRaise;

    [SerializeField]
    private GameEventSO gameEventToListen;
  
    private void Awake()
    {
        StartCoroutine(TestDialog());
        gameEventToListen.EventRaised += () => Debug.Log("GameEvent Raised!");
    }

    private IEnumerator TestDialog()
    {
        yield return new WaitForSeconds(2f);
        dialogManager.StartDialog(dialogGraph);
        
    }

    private void Update()
    {
        if (shouldRaise)
        {
            shouldRaise = false;
            gameEventToRaise.TryRaiseEvent();
        }
    }

    private void TestTextCompressor()
    {
        // TextCompressor.InitializeTextCompressor();
        string uncompressedText = "123487917209 90812374908 172389064612740980 580921384 11";
        string compressedText = TextCompressor.GetCompressedText(uncompressedText);
        TextCompressor.TryGetDecompressedText(compressedText, out string decompressedText);
        Debug.Log(uncompressedText);
        Debug.Log(compressedText);
        Debug.Log(decompressedText);

        string compressedText2 = TextCompressor.GetCompressedText(uncompressedText);
        Debug.Log(compressedText2);
    }
}
