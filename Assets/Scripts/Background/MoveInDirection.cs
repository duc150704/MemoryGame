using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] Vector2 _direction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        this.transform.Translate(_direction * _moveSpeed * Time.deltaTime);
    }
}
