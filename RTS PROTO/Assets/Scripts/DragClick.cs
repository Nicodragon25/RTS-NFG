using UnityEngine;

public class DragClick : MonoBehaviour
{
    Camera myCam;
    [SerializeField] RectTransform boxVisual;
    [SerializeField] RectTransform deselectBoxVisual;
    private Rect selectionBox;

    Vector2 startPosition;
    Vector2 endPosition;
    void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
        DrawDeselectVisual();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
        }

        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;

            if (!Input.GetKey(KeyCode.LeftControl)) DrawVisual();
            else DrawDeselectVisual();
            DrawSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                SelectUnits(true);
                startPosition = Vector2.zero;
                endPosition = Vector2.zero;
                DrawVisual();
            }
            else
            {
                SelectUnits(false);
                startPosition = Vector2.zero;
                endPosition = Vector2.zero;
                DrawDeselectVisual();
            }
            
        }

    }

    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        boxVisual.sizeDelta = boxSize;
    }
    void DrawDeselectVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        deselectBoxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        deselectBoxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        if (Input.mousePosition.x < startPosition.x)
        {
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < startPosition.y)
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    void SelectUnits(bool select)
    {
        foreach (var unit in UnitSelections.Instance.unitList)
        {
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                if (select) UnitSelections.Instance.DragSelect(unit);
                else UnitSelections.Instance.DragDeselect(unit);
            }

        }
    }
}
