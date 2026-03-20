using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ImageMover : MonoBehaviour
{
    [Header("Window Setup")]
    [SerializeField] private GameObject window;
    [SerializeField] private RectTransform windowRectTransform;
    [SerializeField] private CanvasGroup windowCanvasGroup;

    [Header("Screens")]
    [SerializeField] private RectTransform[] screens = new RectTransform[3];

    [Header("Swiping Settings")]
    [SerializeField] private float screenHeight = 0f;

    // Screen Variables
    private int crntIndex = 0;
    private int prevIndex;
    private int nxtIndex;
    private bool isAnim = false;

    //public enum AnimateToDirection
    //{
    //    Top,
    //    Bottom,
    //    Left,
    //    Right
    //}

    //[Header("Animation Setup")]
    //[SerializeField] private AnimateToDirection openDirect = AnimateToDirection.Top;
    //[SerializeField] private AnimateToDirection closeDirect = AnimateToDirection.Bottom;
    //[Space]
    //[SerializeField] private Vector2 distToAnim = new Vector2(x:100, y:100);
    //[SerializeField] private AnimationCurve easingCurve = AnimationCurve.EaseInOut(timeStart: 0, valueStart: 0, timeEnd: 1, valueEnd: 1);
    [Range(0, 1f)] [SerializeField] private float animDuration = 0.5f;

    private bool isOpen;
    private Vector2 initialPos;
    private Vector2 crntPos;
    
    private Vector2 upOffset;
    private Vector2 downOffset;
    private Vector2 leftOffset;
    private Vector2 rightOffset;

    //private Coroutine animateWindowCoroutine;

    //public static event Action OnOpenWindow;
    //public static event Action OnCloseWindow;

    // Location Testing
    //[Header("Helpers")] 
    //[SerializeField] private bool displayGizmos = true;

    //private enum DisplayGizmosAtLoc
    //{
    //    Open,
    //    Close,
    //    Both,
    //    Situational
    //}

    //[SerializeField] private DisplayGizmosAtLoc gizmoHandler;
    //[SerializeField] private Color gizmoOpenColor = Color.green;
    //[SerializeField] private Color gizmoCloseColor = Color.red;
    //[SerializeField] private Color gizmoInitialLocationColor = Color.grey;

    //private Vector2 windowOpenPositionForGizmos;
    //private Vector2 windowClosePositionForGizmos;
    //private Vector2 initialPositionForGizmos;

    // Performs After Data Modified In Editor
    //private void OnValidate()
    //{
    //    //// Grabs Values Once Set In Inspector
    //    //if (window != null)
    //    //{
    //    //    windowRectTransform = GetComponent<RectTransform>();
    //    //    windowCanvasGroup = GetComponent<CanvasGroup>();
    //    //}

    //    // Prevents Beloew 0
    //    distToAnim.x = Mathf.Max(a: 0, b: distToAnim.x);
    //    distToAnim.y = Mathf.Max(a: 0, b: distToAnim.y);

    //    initialPos = windowRectTransform.anchoredPosition;

    //    RecalculateGizmoPositions();
    //}


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Store Ref to Inital Pos
        //initialPos = windowRectTransform.anchoredPosition;

        // Screen Positions
        screens[0].anchoredPosition = Vector2.zero;  // top screen
        screens[1].anchoredPosition = new Vector2(0f, screenHeight);     // middle screen
        screens[2].anchoredPosition = new Vector2(0f, -screenHeight);  // last screen

        //InitializeOffsetPositions();
    }

    // SWIPING
    public void SwipeUp()
    {
        // Check For Already Swiping
        if (isAnim == true)
            return;

        // Start Animation
        StartCoroutine(AnimateSwipe(1));
    }

    public void SwipeDown()
    {
        // Check For Already Swiping
        if (isAnim == true)
            return;

        // Start Animation
        StartCoroutine(AnimateSwipe(0));
    }

    //void InitializeOffsetPositions()
    //{
    //    upOffset = new Vector2(0, distToAnim.y);
    //    downOffset = new Vector2(0, -distToAnim.y);
    //    leftOffset = new Vector2(-distToAnim.x, 0);
    //    rightOffset = new Vector2(+distToAnim.x, 0);
    //}

    //[ContextMenu("Toggle Open Close")]
    //public void ToggleOpenClose()
    //{
    //    if (isOpen)
    //        CloseWindow();
    //    else
    //        OpenWindow();
    //}

    //[ContextMenu("Open Window")]
    //public void OpenWindow()
    //{
    //    if (isOpen)
    //        return;

    //    isOpen = true;
    //    OnOpenWindow?.Invoke();

    //    if (animateWindowCoroutine != null)
    //        StopCoroutine(animateWindowCoroutine);

    //    // Assign New Coroutine, Without Could Override Animation
    //    animateWindowCoroutine = StartCoroutine(AnimateWindow(true));
    //}

    //[ContextMenu("Close Window")]
    //public void CloseWindow()
    //{
    //    if (!isOpen)
    //        return;
    //    isOpen = false;
    //    OnCloseWindow?.Invoke();

    //    if (animateWindowCoroutine != null)
    //        StopCoroutine(animateWindowCoroutine);

    //    // Assign New Coroutine, Without Could Override Animation
    //    animateWindowCoroutine = StartCoroutine(AnimateWindow(false));
    //}

    //private Vector2 GetOffset(AnimateToDirection direction)
    //{
    //    // Grabs Direction And Give Offset
    //    switch(direction)
    //    {
    //        case AnimateToDirection.Top:
    //            return upOffset;
    //        case AnimateToDirection.Bottom:
    //            return downOffset;
    //        case AnimateToDirection.Left:
    //            return leftOffset;
    //        case AnimateToDirection.Right:
    //            return rightOffset;
    //        default:
    //            return Vector2.zero;
    //    };
    //}

    // Animate Swiping
    private IEnumerator AnimateSwipe(int direction) // direction, 0 = Swipe Up | 1 = Swipe Down
    {
        // Set Animation Flag
        isAnim = true;

        // Create Temp Transforms
        RectTransform centerScreen = screens[crntIndex];
        RectTransform incomeScreen; // screen moving to middle
        RectTransform teleportScreen;   // screen teleporting to end or top of screen

        // Set Positions
        Vector2 centerPos = Vector2.zero;
        Vector2 topPos = new Vector2(0f, screenHeight);
        Vector2 btmPos = new Vector2(0f, -screenHeight);

        // Set Indices
        prevIndex = (crntIndex - 1 + 3) % 3;
        nxtIndex = (crntIndex + 1 + 3) % 3;

        // SET TARGET POSITION
        Vector2 crntTrgt, incomePos, incomeTrgt;

        // Swipe Up
        if(direction == 1)
        {
            // Set Positions
            crntTrgt = topPos;
            incomeTrgt = centerPos;
            incomePos = btmPos;

            // Set Screens
            incomeScreen = screens[nxtIndex];
            teleportScreen = screens[prevIndex];

            // Teleport Screen To Bottom
            teleportScreen = screens[prevIndex];
            teleportScreen.anchoredPosition = btmPos;

            // Change Index
            crntIndex = nxtIndex;
        }
        // Swipe Down
        else
        {
            // Set Target Positions
            crntTrgt = btmPos;
            incomeTrgt = centerPos;
            incomePos = topPos;

            // Set Screens
            incomeScreen = screens[prevIndex];
            teleportScreen = screens[nxtIndex];

            // Teleport Screen To Top
            teleportScreen = screens[nxtIndex];
            teleportScreen.anchoredPosition = topPos;

            // Change Index
            crntIndex = prevIndex;
        }

        // Animate Swiping
        float elapsedTime = 0;
        while (elapsedTime < animDuration)
        {
            // Convert Seconds To Percent (Lerp Requires It)
            float timeAsPercent = elapsedTime / animDuration;

            // Moves Position Smoothly
            centerScreen.anchoredPosition = Vector2.Lerp(Vector2.zero, crntTrgt, timeAsPercent);
            incomeScreen.anchoredPosition = Vector2.Lerp(incomePos, incomeTrgt, timeAsPercent);

            // Increase Time
            elapsedTime += Time.deltaTime;

            // Waits A Frame
            yield return null;
        }

        // Force Move Screen (In Case Of Float Misalignment)
        centerScreen.anchoredPosition = crntTrgt;
        incomeScreen.anchoredPosition = incomeTrgt;

        isAnim = false;
    }

    //private IEnumerator AnimateWindow(bool open)
    //{
    //    // Opens Window
    //    if(open)
    //        window.gameObject.SetActive(true);

    //    crntPos = windowRectTransform.anchoredPosition;

    //    float elapsedTime = 0;

    //    Vector2 trgtPos = crntPos;

    //    // Initialization
    //    if(open)
    //        trgtPos = crntPos + GetOffset(openDirect);
    //    else
    //        trgtPos = crntPos - GetOffset(closeDirect);

    //    // Moves To Target Location Over Animation Duration
    //    while(elapsedTime < animDuration)
    //    {
    //        float evaluationAtTime = easingCurve.Evaluate(elapsedTime / animDuration);

    //        // Moves Position Smoothly
    //        windowRectTransform.anchoredPosition = Vector2.Lerp(crntPos, trgtPos, evaluationAtTime);

    //        // Changes Alpha Depending On Curve
    //        //windowCanvasGroup.alpha = open
    //        //    ? Mathf.Lerp(0f, 1f, evaluationAtTime)
    //        //    : Mathf.Lerp(1f, 0f, evaluationAtTime);

    //        elapsedTime += Time.deltaTime;

    //        // Waits A Frame
    //        yield return null;
    //    }

    //    // Set Position In Case Of Position Not Being in Correct Spot (Floats)
    //    windowRectTransform.anchoredPosition = trgtPos;

    //    windowCanvasGroup.alpha = open ? 1 : 0;
    //    windowCanvasGroup.interactable = open;
    //    windowCanvasGroup.blocksRaycasts = open;

    //    // Checks For Window Closed
    //    if(!open)
    //    {
    //        // Resets
    //        window.gameObject.SetActive(false);
    //        windowRectTransform.anchoredPosition = initialPos;
    //    }
    //}

    //private void Refresh()
    //{
    //    OnValidate();
    //}

    //private void RecalculateGizmoPositions()
    //{
    //    InitializeOffsetPositions();

    //    initialPositionForGizmos = new Vector2(windowRectTransform.anchoredPosition.x, windowRectTransform.anchoredPosition.y) + windowRectTransform.rect.center;
    //    windowOpenPositionForGizmos = initialPositionForGizmos + GetOffset(openDirect);
    //    windowClosePositionForGizmos = windowOpenPositionForGizmos + GetOffset(closeDirect);
    //}

    //private void OnDrawGizmos()
    //{
    //    // Check If Want To Display Gizmos
    //    if (!displayGizmos)
    //        return;

    //    if (window == null)
    //        return;

    //    if (windowRectTransform == null)
    //        return;

    //    // Set Color And Draw Shape
    //    Gizmos.color = gizmoInitialLocationColor;
    //    Gizmos.DrawWireCube(initialPositionForGizmos, windowRectTransform.sizeDelta);

    //    // Convert To Local Coords For Transforms (Rotation
    //    Gizmos.matrix = windowRectTransform.parent.localToWorldMatrix;

    //    switch(gizmoHandler)
    //    {
    //        // Draw At Open Position
    //        case DisplayGizmosAtLoc.Open:
    //            DrawCube(windowOpenPositionForGizmos, true);
    //            break;
    //        // Draw At Close
    //        case DisplayGizmosAtLoc.Close:
    //            DrawCube(windowClosePositionForGizmos, false);
    //            break;
    //        // Draw At Both
    //        case DisplayGizmosAtLoc.Both:
    //            DrawCube(windowOpenPositionForGizmos, true);
    //            DrawCube(windowClosePositionForGizmos, false);
    //            break;
    //        // Only Draw At Next Position
    //        case DisplayGizmosAtLoc.Situational:
    //            if (isOpen)
    //                DrawCube(windowClosePositionForGizmos, true);
    //            else
    //                DrawCube(windowOpenPositionForGizmos, false);
    //            break;
    //    };

    //    DrawIndicators();
    //}

    //private void DrawCube(Vector2 windowPosition, bool opens)
    //{
    //    // Checks If It Handles Open Or Close And Sets Color
    //    Gizmos.color = opens ? gizmoOpenColor : gizmoCloseColor;
    //    // Draw Wire Cube With Correct Position And Size
    //    Gizmos.DrawWireCube(windowPosition, windowRectTransform.sizeDelta);
    //}

    //private void DrawIndicators()
    //{
    //    // Creates Line From Open Position And Colors It
    //    Gizmos.color = gizmoOpenColor;
    //    Gizmos.DrawLine(initialPositionForGizmos, windowOpenPositionForGizmos);

    //    Gizmos.color = gizmoCloseColor;
    //    Gizmos.DrawLine(initialPositionForGizmos, windowClosePositionForGizmos);
    //}
}
