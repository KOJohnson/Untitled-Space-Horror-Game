using UnityEngine;

namespace Core
{
    public static class ExtensionMethods
    {
        public static void ResetRotation(this Transform transform, Vector3 startRotation)
        {
            transform.localEulerAngles = startRotation;
        }
    }
}