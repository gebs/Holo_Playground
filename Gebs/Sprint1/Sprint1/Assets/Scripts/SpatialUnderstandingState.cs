using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialUnderstandingState : Singleton<SpatialUnderstandingState>, IInputClickHandler, ISourceStateHandler
{
    public ObjectPlacer Placer;

    public TextMesh DebugDisplay;
    public TextMesh DebugSubDisplay;

    public float MinAreaForStats = 5.0f;
    public float MinAreaForComplete = 50.0f;
    public float MinHorizAreaForComplete = 25.0f;
    public float MinWallAreaForComplete = 10.0f;

    private uint trackedHandsCount = 0;
    private bool ready = false;

    private bool _triggered;

    public bool HideText { get; set; }

    private string _spaceQueryDescription;

    public string SpaceQueryDescription { get { return _spaceQueryDescription; } set { _spaceQueryDescription = value; } }

    public bool DoesScanMeetMinBarForCompletion
    {
        get
        {
            if ((SpatialUnderstanding.Instance.ScanState != SpatialUnderstanding.ScanStates.Scanning) || (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding))
                return false;

            IntPtr statsPtr = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceStatsPtr();

            if (SpatialUnderstandingDll.Imports.QueryPlayspaceStats(statsPtr) == 0)
                return false;

            SpatialUnderstandingDll.Imports.PlayspaceStats stats = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceStats();

            if ((stats.TotalSurfaceArea > MinAreaForComplete) || (stats.HorizSurfaceArea > MinHorizAreaForComplete) || (stats.WallSurfaceArea > MinWallAreaForComplete))
                return true;

            return false;
        }
    }


    public string PrimaryText
    {
        get
        {
            if (HideText)
                return string.Empty;

            if (!string.IsNullOrEmpty(SpaceQueryDescription))
                return SpaceQueryDescription;

            if (SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
            {
                switch (SpatialUnderstanding.Instance.ScanState)
                {
                    case SpatialUnderstanding.ScanStates.Scanning:
                        IntPtr statsPtr = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceStatsPtr();
                        if (SpatialUnderstandingDll.Imports.QueryPlayspaceStats(statsPtr) == 0)
                            return "playspace stats query failed";

                        if (DoesScanMeetMinBarForCompletion)
                        {
                            return "Wedà fertig bisch chasch Air Tappà zum abschliesse :)";
                        }

                        return "Louf no chlei umà!!!";
                    case SpatialUnderstanding.ScanStates.Finishing:
                        return "Scan am fertig machà, tuà churz wartà";
                    case SpatialUnderstanding.ScanStates.Done:
                        return "Bi fertig mit scannà";
                    default:
                        return "Scànstatus = " + SpatialUnderstanding.Instance.ScanState;
                }
            }
            return string.Empty;
        }
    }
    public Color PrimaryColor
    {
        get
        {
            ready = DoesScanMeetMinBarForCompletion;

            if (SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.Scanning)
            {
                if (trackedHandsCount > 0)
                    return ready ? Color.green : Color.red;
                else
                    return ready ? Color.yellow : Color.white;
            }

            float alpha = 1.0f;

            return (!string.IsNullOrEmpty(SpaceQueryDescription)) ?
                 (PrimaryText.Contains("processing") ? new Color(1.0f, 0.0f, 0.0f, 1.0f) : new Color(1.0f, 0.7f, 0.1f, alpha)) :
                new Color(1.0f, 1.0f, 1.0f, alpha);
        }
    }

    public string DetailText
    {
        get
        {
            if (SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.None)
                return "";

            if ((SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.Scanning) && (SpatialUnderstanding.Instance.AllowSpatialUnderstanding))
            {
                IntPtr statPtr = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceStatsPtr();
                if (SpatialUnderstandingDll.Imports.QueryPlayspaceStats(statPtr) == 0)
                    return "Playspace stats query failed";

                SpatialUnderstandingDll.Imports.PlayspaceStats stats = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceStats();

                if (stats.TotalSurfaceArea > MinAreaForStats)
                {
                    SpatialMappingManager.Instance.DrawVisualMeshes = false;
                    string subDisplayText = string.Format("totalArea={0:0.0}, horiz={1:0.0}, wall={2:0.0}", stats.TotalSurfaceArea, stats.HorizSurfaceArea, stats.WallSurfaceArea);
                    subDisplayText += string.Format("\nnumFloorCells={0}, numCeilingCells={1}, numPlatformCells={2}", stats.NumFloor, stats.NumCeiling, stats.NumPlatform);
                    subDisplayText += string.Format("\npaintMode={0}, seenCells={1}, notSeen={2}", stats.CellCount_IsPaintMode, stats.CellCount_IsSeenQualtiy_Seen + stats.CellCount_IsSeenQualtiy_Good, stats.CellCount_IsSeenQualtiy_None);
                    return subDisplayText;
                }
                return "";

            }
            return "";
        }
    }

    public void Update_DebugDisplay()
    {
        if (DebugDisplay == null)
            return;

        DebugDisplay.text = PrimaryText;
        DebugDisplay.color = PrimaryColor;

        DebugSubDisplay.text = DetailText;
    }
    private void Update()
    {
        Update_DebugDisplay();

        if (!_triggered && SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.Done)
        {
            _triggered = true;
            Placer.CreateScene();
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (ready &&
           (SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.Scanning) &&
           !SpatialUnderstanding.Instance.ScanStatsReportStillWorking)
        {
            SpatialUnderstanding.Instance.RequestFinishScan();
        }
    }

    void ISourceStateHandler.OnSourceDetected(SourceStateEventData eventData)
    {
        trackedHandsCount++;
    }

    void ISourceStateHandler.OnSourceLost(SourceStateEventData eventData)
    {
        trackedHandsCount--;
    }

}
