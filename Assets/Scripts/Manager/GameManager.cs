using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RightMouseClick();
    }

    void RightMouseClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePositon.z = -5f;
            if (Physics.Raycast(mousePositon, new Vector3(0f, 0f, 1f), out RaycastHit obj, 10f) && obj.collider.gameObject.tag == "Card")
            {
                obj.collider.gameObject.GetComponent<Flip>().FlipObject();
            }
        }
    }
}
