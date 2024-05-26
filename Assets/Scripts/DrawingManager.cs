using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DrawingManager : MonoBehaviour
{
    public RawImage canvasImage;
    public Color drawColor = Color.black;
    public int brushSize = 5;

    private Texture2D texture;
    private bool isDrawing = false;
    private Vector2 mousePosition;

    // Input Action reference
    public InputAction drawAction;

    void Start()
    {
        // Create a new texture and apply it to the RawImage
        texture = new Texture2D((int)canvasImage.rectTransform.rect.width, (int)canvasImage.rectTransform.rect.height);
        canvasImage.texture = texture;
        ClearCanvas();

        // Enable the input action
        drawAction.Enable();
        drawAction.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            isDrawing = true;
            Draw(mousePosition);
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDrawing = false;
        }
        if (isDrawing)
        {
            Draw(mousePosition);
        }
    }

    void Draw(Vector2 position)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasImage.rectTransform, position, null, out Vector2 localPoint);
        if (IsWithinBounds(localPoint))
        {
            int x = (int)(localPoint.x + canvasImage.rectTransform.rect.width / 2);
            int y = (int)(localPoint.y + canvasImage.rectTransform.rect.height / 2);

            for (int i = 0; i < brushSize; i++)
            {
                for (int j = 0; j < brushSize; j++)
                {
                    texture.SetPixel(x + i, y + j, drawColor);
                }
            }
            texture.Apply();
        }
    }

    bool IsWithinBounds(Vector2 localPoint)
    {
        return localPoint.x >= -canvasImage.rectTransform.rect.width / 2 &&
               localPoint.x < canvasImage.rectTransform.rect.width / 2 &&
               localPoint.y >= -canvasImage.rectTransform.rect.height / 2 &&
               localPoint.y < canvasImage.rectTransform.rect.height / 2;
    }

    public void ClearCanvas()
    {
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                texture.SetPixel(x, y, Color.white);
            }
        }
        MakeWhiteTransparent(texture);
        texture.Apply();

    }
    Texture2D MakeWhiteTransparent(Texture2D texture)
    {
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                Color pixelColor = texture.GetPixel(x, y);
                if (pixelColor.r >= 0.9f && pixelColor.g >= 0.9f && pixelColor.b >= 0.9f)
                {
                    pixelColor.a = 0f; // 흰색 픽셀의 알파 값을 0으로 설정하여 투명하게 만듭니다.
                    texture.SetPixel(x, y, pixelColor);
                }
            }
        }
        texture.Apply();
        return texture;
    }
}
