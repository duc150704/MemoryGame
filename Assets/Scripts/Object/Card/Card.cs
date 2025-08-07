using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Card : MonoBehaviour
{
    public string Value { get; private set; }
    [SerializeField] SpriteRenderer _frontCard;
    [SerializeField] SpriteRenderer _backCard;

    [SerializeField] float _timeToFlip;
    bool _isFlipping = false;
    bool _isFacingUp = false;
    public bool IsFlipping => _isFlipping;
    public bool IsFacingUp => _isFacingUp;

    public void SetupData(Sprite front, Sprite back, string value)
    {
        _frontCard.sprite = front;
        _backCard.sprite = back;
        Value = value;
    }

    public void MoveTo(Vector3 position, float timeToMove)
    {
        StartCoroutine(Move(position, timeToMove));
    }

    IEnumerator Move(Vector3 position, float timeToMove)
    {
        float timeToMoveCounter = 0f;
        Vector3 gameObjPosition = gameObject.transform.position;
        while (timeToMoveCounter < timeToMove)
        {
            gameObject.transform.position = Vector3.Lerp(gameObjPosition, position, timeToMoveCounter / timeToMove);
            timeToMoveCounter += Time.deltaTime;
            yield return null;
        }
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
        _isFacingUp = _isFacingUp ? false : true;
    }

    public void Flip()
    {
        if (_isFlipping)
            return;
        StartCoroutine(SmoothFlip());
    }

    public void Disappear(float time)
    {
        StartCoroutine(DisappearObject(time));
    }

    IEnumerator DisappearObject(float time)
    {
        Vector3 originScale = transform.localScale;
        float timeCounter = 0;
        while(timeCounter <= time)
        {
            transform.localScale = Vector3.Lerp(originScale, Vector3.zero, timeCounter / time);
            timeCounter += Time.deltaTime;
            yield return null; 
        }
        Destroy(gameObject);
    }
}
