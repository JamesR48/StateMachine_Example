using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// This class handles the targeting system. Sets a new target on mouse click and sends out a notification that
/// other classes can listen to know when a new target has been set
/// </summary>

public class TargetAssigner : MonoBehaviour
{
    public event Action<Vector3> NewTargetAcquired = delegate { };

    [SerializeField] GameObject TargetIndicatorPrefab = null;
    GameObject TargetIndicatorGO;

    [SerializeField] Bot_StateMachine BotSMRef;

    Camera Cam = null;
    RaycastHit RayHit;

    private void Awake()
    {
        // get references
        Cam = Camera.main;
        // setup the target indicator visual and hide it
        TargetIndicatorGO = Instantiate(TargetIndicatorPrefab, RayHit.point, Quaternion.identity);
        TargetIndicatorGO.SetActive(false);
    }

    void Update()
    {
        // LEFT CLICK - set new target (unless Bot's current State is 'Found')
        if (Input.GetMouseButtonDown(0) && (BotSMRef.CurrentState != BotSMRef.FoundState))
        {
            GetNewMouseHit(Cam);
            SetNewTargetPoint(new Vector3(RayHit.point.x, 0.27f, RayHit.point.z));
        }
    }

    public void GetNewMouseHit(Camera camera)
    {
        // get a ray hit point from camera click location
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RayHit, Mathf.Infinity))
        {
            Debug.Log("Ray hit: " + RayHit.transform.name
                + " at coordinates: " + RayHit.point);
        }
    }

    public void SetNewTargetPoint(Vector3 position)
    {
        // handle the target visual
        TargetIndicatorGO.SetActive(true);
        TargetIndicatorGO.transform.position = position;
        // send out event notification
        NewTargetAcquired.Invoke(position);
    }
}
