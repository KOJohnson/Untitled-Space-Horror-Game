using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTrail : MonoBehaviour
{
    [SerializeField]private float time;
    private void Start()
    {
        StartCoroutine(DisableGameObject());
    }

    private IEnumerator DisableGameObject()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
