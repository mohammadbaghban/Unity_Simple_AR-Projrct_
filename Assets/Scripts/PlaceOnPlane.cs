using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class PlaceOnPlane : MonoBehaviour
{
    ARRaycastManager arRaycastManager;
    public List<ARRaycastHit> hits;
    private bool touched = false;
    private bool made = false;
    public GameObject model;
    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        hits = new List<ARRaycastHit>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } 
        if (Input.touchCount == 0)
        {
            touched = false;
            return;
        }

        Touch touch = Input.GetTouch(0);

        if (IsPointerOverUIObject(touch.position))
        {
            return;
        }
        if (arRaycastManager.Raycast(touch.position, hits) && !touched)
        {
            Pose pose = hits[0].pose;

            GameObject newObject = Instantiate(model, pose.position, pose.rotation, model.transform.parent);
            newObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            // made = true;

        }
        touched = true;

        
    }

    bool IsPointerOverUIObject(Vector2 pos)
    {
        if (EventSystem.current == null)
        {
            return false;
        }
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(pos.x, pos.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
