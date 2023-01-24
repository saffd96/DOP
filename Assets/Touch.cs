using System.Collections;
using UnityEngine;

public class Touch : MonoBehaviour
{
   [SerializeField] private GameObject linePrefab;
   [SerializeField] private Camera mainCam;
   [SerializeField] private Eraser eraserScript;
   
    private Coroutine _drawing;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartLine();
        }

        if (Input.GetMouseButtonUp(0))
        {
            FinishLine();
        }
    }

    private void StartLine()
    {
        if (_drawing != null)
        {
            StopCoroutine(_drawing);
        }

        _drawing = StartCoroutine(DrawLine());
    }

    private void FinishLine()
    {
        if (_drawing != null)
            StopCoroutine(_drawing);
    }

    private IEnumerator DrawLine()
    {
        var newLine = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        LineRenderer line = newLine.GetComponent<LineRenderer>();
        line.positionCount = 0;
        while (true)
        {
            Vector3 position = mainCam.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, position);
            eraserScript.AssignScreenAsMask();
            yield return null;
        }
    }
}