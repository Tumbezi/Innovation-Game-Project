using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkyboxColor
{
    Red = 0,
    Green = 1,
    Blue = 2,
    Yellow = 3

}
public class SkyboxManager : MonoBehaviour
{
    [SerializeField]
    Material[] skyboxMaterials;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSkybox()
    {
        if (index >= skyboxMaterials.Length - 1)
            index = 0;
        else
            index++;
        RenderSettings.skybox = skyboxMaterials[index];
        //(int) SkyboxColor
    }
}
