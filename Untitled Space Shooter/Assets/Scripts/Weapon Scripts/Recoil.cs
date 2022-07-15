using UnityEngine;
using Random = UnityEngine.Random;

public class Recoil : MonoBehaviour
{
    private Vector3 _currentRotation;
    private Vector3 _targetRotation;

    public void GunRotation(float returnSpeed, float snappiness)
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(_currentRotation);
    }

    public void RecoilFire(float recoilX,float recoilY,float recoilZ)
    {
        _targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
