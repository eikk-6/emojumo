using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;
using System.Collections;

public class ImageComparer : MonoBehaviour
{
    public Button compareButton;
    public string imagePath1;  // 첫 번째 이미지 경로
    public string imagePath2;  // 두 번째 이미지 경로

    void Start()
    {
        compareButton.onClick.AddListener(OnCompareButtonClick);
    }

    void OnCompareButtonClick()
    {
        StartCoroutine(CompareImagesCoroutine());
    }

    IEnumerator CompareImagesCoroutine()
    {

        yield return new WaitForEndOfFrame();

        Mat img1 = Cv2.ImRead(imagePath1, ImreadModes.Color);
        Mat img2 = Cv2.ImRead(imagePath2, ImreadModes.Color);

        if (img1.Empty() || img2.Empty())
        {
            Debug.LogError("이미지를 불러올 수 없습니다!");
            yield break;
        }

        double similarity = CompareTwoImages(img1, img2);
        Debug.Log("유사도: " + similarity);
    }

    double CompareTwoImages(Mat img1, Mat img2)
    {
        // 이미지 크기를 동일하게 조정
        Cv2.Resize(img1, img1, new Size(256, 256));
        Cv2.Resize(img2, img2, new Size(256, 256));

        // 그레이스케일 변환
        Mat grayImg1 = new Mat();
        Mat grayImg2 = new Mat();
        Cv2.CvtColor(img1, grayImg1, ColorConversionCodes.BGR2GRAY);
        Cv2.CvtColor(img2, grayImg2, ColorConversionCodes.BGR2GRAY);

        // 히스토그램 계산
        Mat histImg1 = new Mat();
        Mat histImg2 = new Mat();
        Cv2.CalcHist(new Mat[] { grayImg1 }, new int[] { 0 }, null, histImg1, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });
        Cv2.CalcHist(new Mat[] { grayImg2 }, new int[] { 0 }, null, histImg2, 1, new int[] { 256 }, new Rangef[] { new Rangef(0, 256) });

        // 히스토그램 정규화
        Cv2.Normalize(histImg1, histImg1, 0, 1, NormTypes.MinMax);
        Cv2.Normalize(histImg2, histImg2, 0, 1, NormTypes.MinMax);

        // 유사도 계산 (코사인 유사도 사용)
        double similarity = Cv2.CompareHist(histImg1, histImg2, HistCompMethods.Correl);

        return similarity;
    }
}
