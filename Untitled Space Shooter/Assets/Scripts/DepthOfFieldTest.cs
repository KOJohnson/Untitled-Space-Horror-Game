using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[ExecuteInEditMode]
public class DepthOfFieldTest : MonoBehaviour
{
    public Volume volume;

    private VolumeProfile volumeProfile;

    public Transform focusObject;
    // Start is called before the first frame update
    void Start()
    {
        volumeProfile = volume.sharedProfile;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!volumeProfile.TryGet<DepthOfField>(out var depthOfField))
        {
            depthOfField = volumeProfile.Add<DepthOfField>(true);
        }
        
    }
}
