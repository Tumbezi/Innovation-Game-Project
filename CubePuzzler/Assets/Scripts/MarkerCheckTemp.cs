using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Edgeside
{
    None,
    Left,
    Right,
    Top,
    Bottom
}

public class MarkerCheckTemp : MonoBehaviour
{
    public bool isStart, isEnd, isEdge, isLever;
    public Edgeside edge;
    public Transform[] sides;
    public GateController gate;

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
        gate.GateAction();
    }
}
