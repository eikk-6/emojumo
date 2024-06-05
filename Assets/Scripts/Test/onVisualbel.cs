using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class onVisualbel : MonoBehaviour
{
    
    [SerializeField] GameObject staff;
    [SerializeField] DrawingManager drawingManager;
   
    InputDevice left;
    InputDevice right;
    private bool onDraw = false;
    private bool isButtonA = false;
    [SerializeField] GameObject GameObject;
    private bool isButtonB = false;
    private bool isButtonX = false;
    private bool isButtonY = false;
    private bool isLTriggerPressed = false;

    private bool prevButtonA;
    private bool prevButtonB;
    private bool prevButtonX;
    private bool prevButtonY;
    private bool prevButtonLT;

    [SerializeField]
    private GameObject Set;
    [SerializeField]
        private GameObject FiringVFX1;
        [SerializeField]
        private GameObject FiringVFX2;
        [SerializeField]
        private GameObject FiringVFX3;
        [SerializeField]
        private GameObject FiringVFX4;
        [SerializeField]
        private GameObject FiringVFX;


    public Texture2D referenceTexture1;
    public Texture2D referenceTexture2;
    public Texture2D referenceTexture3;
    public Texture2D referenceTexture4;
    public Texture2D referenceTexture;

    [SerializeField]
    private GameObject ChargePoint;
   
    private float stayTime = 0.0f;
    public float requiredTime = 5.0f;
    public float similarity;
    public int ran; 
    private bool isTriggerEnabled = true;
   
    GameObject chargingVFXInstance;
    RaycastHit hit;
   
   
    public RawImage userDrawnImage; // 유저가 그린 이미지
     // 비교할 대상 이미지
    public Button compareButton;
    public Text resultText;
    public RawImage overlayImage; // 반투명하게 덮을 RawImage

    public Texture2D texture;
   
    [Range(1, 1000)]
    public int sampleSize = 500; // 샘플링할 픽셀 수
    [Range(0, 10)]
    public int neighborhoodSize = 1; // 비교할 범위

    // Start is called before the first frame update
    void Start()
    {
        prevButtonA = false;
        prevButtonB = false;
        prevButtonX = false;
        prevButtonY = false;
        prevButtonLT = false;

        if (overlayImage != null && referenceTexture != null)
        {
            overlayImage.texture = referenceTexture;
            overlayImage.color = new Color(1f, 1f, 1f, 0.5f); // 반투명 설정
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        ButtonInput();
    }

    void ButtonInput()
    {
        // 좌우 디바이스 인식
        right = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        left = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        // 순차적으로 A, B, X, Y의 버튼 인식 및 작동
        right.TryGetFeatureValue(CommonUsages.primaryButton, out isButtonA);
        if (isButtonA && !prevButtonA)
        {
            
        }
        prevButtonA = isButtonA;

        right.TryGetFeatureValue(CommonUsages.secondaryButton, out isButtonB);
        if (isButtonB && !prevButtonB)
        {
            
        }
        prevButtonB = isButtonB;

        left.TryGetFeatureValue(CommonUsages.primaryButton, out isButtonX);
        if (isButtonX && !prevButtonX)
        {
            CompareImages();
        }
        prevButtonX = isButtonX;

        left.TryGetFeatureValue(CommonUsages.secondaryButton, out isButtonY);
        if (isButtonY && !prevButtonY)
        {
            TriggerPressed();
        }
        prevButtonY = isButtonY;

        right.TryGetFeatureValue(CommonUsages.triggerButton, out bool isRTriggerPressed);
        if (isRTriggerPressed)
        {
            ImageDrawing();
        }
        left.TryGetFeatureValue(CommonUsages.triggerButton, out isLTriggerPressed);
        if (isLTriggerPressed && !prevButtonLT)
        {
            MagicShot();
        }
        prevButtonLT = isLTriggerPressed;

    }
    void TriggerPressed()
    {
        if (GameObject != null)
        {
            GameObject.SetActive(!GameObject.activeSelf);
        }
        
    }
    void ImageDrawing()
    {

    }
    void MagicShot()
    {
        
        if (onDraw && similarity > 0.5)
        {
            Magicshot2();
            onDraw = false;
            similarity = 0;
        }
        drawingManager.ClearCanvas();
    }
    void Magicshot2()
    {
        switch (ran)
        {
            case 0:
                Set = FiringVFX;
                break;
            case 1:
                Set = FiringVFX1;
                break;
            case 2:
                Set = FiringVFX2;
                break;
            case 3:
                Set = FiringVFX3;
                break;
            case 4:
                Set = FiringVFX4;
                break;
        }
        if (isTriggerEnabled)
        {
            Debug.DrawRay(ChargePoint.transform.position, ChargePoint.transform.forward * 30f, Color.red, 1f);

            // isCharging 상태를 유지
            // Destroy the existing charging VFX
            if (chargingVFXInstance != null)
            {
                Destroy(chargingVFXInstance);
                chargingVFXInstance = null;
            }

            // Instantiate the firing VFX at the charge point
            GameObject firingVFXInstance = Instantiate(Set, ChargePoint.transform.position, ChargePoint.transform.rotation);

            // Set the velocity of the firing VFX to move it forward
            Rigidbody rb = firingVFXInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = 10f * ChargePoint.transform.forward;
            }

            // Destroy the firing VFX after 5 seconds
            Destroy(firingVFXInstance, 5f);

            // Disable trigger input for a short duration
            StartCoroutine(DisableInputForSeconds(3.0f));
        }
    }

    IEnumerator DisableInputForSeconds(float seconds)
    {
        isTriggerEnabled = false; // 마법 비활성화
        yield return new WaitForSeconds(seconds); // 지정된 시간 동안 대기
        isTriggerEnabled = true; // 마법 재활성화
    }

    public void CompareImages()
    {
        if (userDrawnImage == null)
        {
            resultText.text = "UserDrawnImage가 설정되지 않았습니다.";
            return;
        }
        if (referenceTexture == null)
        {
            resultText.text = "ReferenceTexture가 설정되지 않았습니다.";
            return;
        }

        Texture2D userTexture = userDrawnImage.texture as Texture2D;

        if (userTexture == null)
        {
            resultText.text = "UserDrawnImage의 텍스처가 유효하지 않습니다.";
            return;
        }

        // 두 이미지를 같은 크기로 리사이즈
        int targetWidth = referenceTexture.width;
        int targetHeight = referenceTexture.height;

        Texture2D resizedUserTexture = ResizeTexture(userTexture, targetWidth, targetHeight);

        similarity = CalculateSimilarity(resizedUserTexture, referenceTexture, sampleSize);
        resultText.text = "유사도: " + (similarity * 100f).ToString("F2") + "%";
        onDraw = true;

        ran = Random.Range(0, 5);

    }

    private Texture2D ResizeTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);
        float scaleX = (float)source.width / targetWidth;
        float scaleY = (float)source.height / targetHeight;

        for (int y = 0; y < targetHeight; y++)
        {
            for (int x = 0; x < targetWidth; x++)
            {
                float px = x * scaleX;
                float py = y * scaleY;
                result.SetPixel(x, y, source.GetPixelBilinear(px / source.width, py / source.height));
            }
        }

        result.Apply();
        return result;
    }

    private float CalculateSimilarity(Texture2D tex1, Texture2D tex2, int sampleSize)
    {
        List<Vector2Int> paintedPixels1 = GetPaintedPixels(tex1);
        List<Vector2Int> paintedPixels2 = GetPaintedPixels(tex2);

        if (paintedPixels1.Count == 0 || paintedPixels2.Count == 0)
        {
            return 0f;
        }

        int similarCount = 0;
        int samples = Mathf.Min(sampleSize, paintedPixels1.Count, paintedPixels2.Count);

        for (int i = 0; i < samples; i++)
        {
            Vector2Int pos1 = paintedPixels1[Random.Range(0, paintedPixels1.Count)];
            Vector2Int pos2 = paintedPixels2[Random.Range(0, paintedPixels2.Count)];

            if (PositionsAreSimilar(pos1, tex1, tex2))
            {
                similarCount++;
            }
        }

        return (float)similarCount / sampleSize;
    }

    private bool PositionsAreSimilar(Vector2Int pos1, Texture2D tex1, Texture2D tex2)
    {
        bool foundSimilar = false;
        for (int y = -neighborhoodSize; y <= neighborhoodSize; y++)
        {
            for (int x = -neighborhoodSize; x <= neighborhoodSize; x++)
            {
                int nx = pos1.x + x;
                int ny = pos1.y + y;

                if (nx >= 0 && nx < tex2.width && ny >= 0 && ny < tex2.height)
                {
                    bool dot1 = tex1.GetPixel(pos1.x, pos1.y).a != 0;
                    bool dot2 = tex2.GetPixel(nx, ny).r == 0 && tex2.GetPixel(nx, ny).g == 0 && tex2.GetPixel(nx, ny).b == 0;

                    if (dot1 && dot2)
                    {
                        foundSimilar = true;
                        break;
                    }
                }
            }
            if (foundSimilar) break;
        }

        return foundSimilar;
    }

    private List<Vector2Int> GetPaintedPixels(Texture2D texture)
    {
        List<Vector2Int> paintedPixels = new List<Vector2Int>();
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color pixelColor = texture.GetPixel(x, y);
                if (pixelColor.a != 0 && pixelColor.r == 0 && pixelColor.g == 0 && pixelColor.b == 0) // 검정색 픽셀을 확인
                {
                    paintedPixels.Add(new Vector2Int(x, y));
                }
            }
        }
        return paintedPixels;
    }
}
