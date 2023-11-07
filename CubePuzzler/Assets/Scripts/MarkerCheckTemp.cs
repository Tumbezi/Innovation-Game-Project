using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarkerCheckTemp : MonoBehaviour
{
    public bool isStart, isEnd, isEdge, isLever, isIce;
    public Transform[] sides;
    public GateController[] gates;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGate()
    {
        foreach (var gate in gates)
        {
            gate.GateAction();
        }
    }
}
