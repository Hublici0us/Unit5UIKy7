using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TrailRenderer))]

public class MouseTrail : MonoBehaviour
{

    private GameManager gameManager;
    private Camera mainCam;
    private Vector3 mousePos;
    private TrailRenderer trail;

    private bool boxing = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainCam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            boxing = true;
            UpdateTrail();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            boxing = false;
            UpdateTrail();
        }
        UpdateMousePos();
    }

    void UpdateMousePos()
    {
        mousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = mousePos;
    }

    void UpdateTrail()
    {
        trail.enabled = boxing;
    }
}
