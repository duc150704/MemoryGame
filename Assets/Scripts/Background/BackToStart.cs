using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToStart : MonoBehaviour
{
    [SerializeField] Vector2 _startPosition;
    [SerializeField] Vector2 _endPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    void Check()
    {
        if(this.transform.position.y >= _endPosition.y)
        {
            this.transform.position = _startPosition;
        }
    }
}
