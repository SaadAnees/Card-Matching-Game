using UnityEngine;
using UnityEngine.UI;

public class LayoutController : MonoBehaviour
{
    public GridLayoutGroup gridLayout;
    public int rows = 2;
    public int columns = 2;

    private void Start()
    {
        AdjustGrid();
    }

    private void AdjustGrid()
    {
        RectTransform rt = gridLayout.GetComponent<RectTransform>();
        float width = rt.rect.width / columns;
        float height = rt.rect.height / rows;

        float size = Mathf.Min(width, height);
        gridLayout.cellSize = new Vector2(size, size);
    }
}
