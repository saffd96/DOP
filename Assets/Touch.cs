using System.Collections;
using UnityEngine;

public class Touch : MonoBehaviour
{
   [SerializeField] private TrailRenderer trailRendererPrefab;
   [SerializeField] private Camera mainCam;
   
    private Coroutine _drawingRoutine;
    private TrailRenderer _currentTrailRenderer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnTrail();
        }

        if (Input.GetMouseButtonUp(0))
        {
            FinishRemoveTrail();
        }
    }

    private void SpawnTrail()
    {
        if (_drawingRoutine != null)
        {
            StopCoroutine(_drawingRoutine);
        }
        
        _currentTrailRenderer = Instantiate(trailRendererPrefab, Vector3.zero, Quaternion.identity);
        _drawingRoutine = StartCoroutine(DrawLine());
    }

    private void FinishRemoveTrail()
    {
        StopCoroutine(_drawingRoutine);
    }

    private IEnumerator DrawLine()
    {
        while (true)
        {
            var a = mainCam.ScreenToWorldPoint(Input.mousePosition);
            var position = new Vector3(a.x, a.y, 10);
            _currentTrailRenderer.transform.position = position;
            yield return null;
        }
    }
}