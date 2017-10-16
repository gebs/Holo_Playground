using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlacer : MonoBehaviour, IInputClickHandler, ISourceStateHandler
{
    private uint trackedHandsCount = 0;
    private bool placement = true;

    public GameObject gameObject;

    // Use this for initialization
    void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (placement)
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;

            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo, 30.0f, SpatialMappingManager.Instance.LayerMask))
            {
                gameObject.transform.parent.position = hitInfo.point;

                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.y = 0;
                gameObject.transform.parent.rotation = toQuat;
            }
        }

    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        placement = !placement;
        SpatialMappingManager.Instance.DrawVisualMeshes = placement;

    }

    public void OnSourceDetected(SourceStateEventData eventData)
    {
        trackedHandsCount++;
    }

    public void OnSourceLost(SourceStateEventData eventData)
    {
        trackedHandsCount--;
    }
}
