using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    [SerializeField] float _timeToFlip;
    bool _isFlipping = false;

    public bool IsFlipping => _isFlipping;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SmoothFlip()
    {
        _isFlipping = true;
        float timeToFlipCounter = 0;
        Quaternion startRotation = this.transform.rotation;
        Quaternion finalRotation = Quaternion.Euler(0f, startRotation.eulerAngles.y + 180f, 0f);
        while (_timeToFlip > 0f && timeToFlipCounter <= _timeToFlip)
        {
            this.transform.rotation = Quaternion.Lerp(startRotation, finalRotation, timeToFlipCounter / _timeToFlip);
            timeToFlipCounter += Time.deltaTime;
            yield return null;
        }
        this.transform.rotation = finalRotation;
        _isFlipping = false;
    }

    public void FlipObject()
    {
        if (_isFlipping)
            return;
        StartCoroutine(SmoothFlip());
    }
}
