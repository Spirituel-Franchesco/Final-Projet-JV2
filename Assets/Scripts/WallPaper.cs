using UnityEngine;

public class WallPaper : MonoBehaviour
{
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _wallGhostPrefab;
    [SerializeField] private LayerMask _placementLayer;
    [SerializeField] private float _checkRadius = 1f;

    private GameObject _ghostInstance;

    void Start()
    {
        _ghostInstance = Instantiate(_wallGhostPrefab);
        _ghostInstance.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, _placementLayer))
        {
            Vector3 position = hit.point;
            position.y = 0; // aligner au sol
            _ghostInstance.transform.position = position;
            _ghostInstance.SetActive(true);

            bool canPlace = !Physics.CheckSphere(position, _checkRadius, LayerMask.GetMask("Wall"));
            _ghostInstance.GetComponent<MeshRenderer>().material.color = canPlace ? Color.green : Color.red;

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                Instantiate(_wallPrefab, position, Quaternion.identity);
            }
        }
        else
        {
            _ghostInstance.SetActive(false);
        }
    }
}
