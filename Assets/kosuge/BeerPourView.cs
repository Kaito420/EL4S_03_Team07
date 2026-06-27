using UnityEngine;

public class BeerPourView : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [Header("Beer Mesh")]
    [SerializeField] private Transform beerBody;

    [Header("Settings")]
    [SerializeField] private float maxFill = 1.00f;
    [SerializeField] private float pourSpeed = 0.2f;
    [SerializeField] private float foamThickness = 0.04f;

    [Header("Offset")]
    [SerializeField] private float beerBaseY = 0.0f;

    private float fillRate = 0.0f;

    private float time = 0.0f;
    private void Start()
    {
        fillRate = 0.0f;

        time = 0.0f;

        ApplyView();
    }

    private void Update()
    {
        bool isPouring = time > 0.5f;

        if (isPouring)
        {
            fillRate += pourSpeed * Time.deltaTime;
            if (fillRate > 1.0f)
            {
                Destroy(particle);
            }
            fillRate = Mathf.Clamp(fillRate, 0.0f, maxFill);

            ApplyView();
        }

        time += Time.deltaTime;
    }

    private void ApplyView()
    {
        // ÉrÅ[Éãñ{ëÃÇÕ 0 Å® 1 Ç≈êLÇŒÇ∑
        Vector3 beerScale = beerBody.localScale;
        beerScale.y = fillRate * 0.75f;
        beerBody.localScale = beerScale;
    }
}