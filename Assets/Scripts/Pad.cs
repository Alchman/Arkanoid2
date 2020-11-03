using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
    public float maxX;

    float yPosition;

    // Start is called before the first frame update
    void Start()
    {
        yPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePixelPosition = Input.mousePosition; //позиция мыши в координатах экрана
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePixelPosition); //позиция мыши в координатах игрового мира
        //Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector3 padNewPosition = mouseWorldPosition;
        //padNewPosition.z = 0;
        //padNewPosition.y = yPosition;

        Vector3 padNewPosition = new Vector3(mouseWorldPosition.x, yPosition, 0);

        padNewPosition.x = Mathf.Clamp(padNewPosition.x, -maxX, maxX);

        transform.position = padNewPosition;
    }
}
