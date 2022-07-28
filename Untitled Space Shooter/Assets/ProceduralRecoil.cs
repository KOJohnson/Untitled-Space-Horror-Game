using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralRecoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private Vector3 initialGunPosition;

    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        initialGunPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void Recoil(float recoilX, float recoilY, float recoilZ, float kickBackZ)
    {
        targetPosition -= new Vector3(0, 0, kickBackZ);
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }

    public void UpdatePositionRotation(float snappiness, float returnAmount)
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * returnAmount);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, Time.fixedDeltaTime * snappiness);
        transform.localRotation = Quaternion.Euler(currentRotation); 
        cam.localRotation = Quaternion.Euler(currentRotation);
        
        targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * returnAmount);
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * snappiness);
        transform.localPosition = currentPosition;
    }
}
