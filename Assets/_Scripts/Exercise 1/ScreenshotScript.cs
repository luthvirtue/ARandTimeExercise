using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenshotScript : MonoBehaviour
{
    public Image pictureHolder;
    public TextMeshProUGUI locationText;

    void Start()
    {
        ShowPictureHolder(false);
    }

    public void ShowPictureHolder(bool visible)
    {
        pictureHolder.gameObject.SetActive(visible);
    }

    //take screenshot and share
    public void TakeScreenshot()
    {
        GleyShare.Manager.CaptureScreenshot(ScreenshotCaptured);
    }

    private void ScreenshotCaptured(Sprite sprite)
    {
        if (sprite != null)
        {
            pictureHolder.sprite = sprite;
            ShowPictureHolder(true);
        }
    }

    public void Cancel()
    {
        ShowPictureHolder(false);
    }

    public void Share()
    {
        GleyShare.Manager.SharePicture();
    }

    //save to gallery
    public void CaptureScreenButton()
    {
        StartCoroutine(CaptureIt());
        TakeScreenshot();
    }

    private IEnumerator CaptureIt()
    {
        string timeStamp = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        string fileName = "SS_" + timeStamp + ".png";
        string savePath =  fileName;
        locationText.text = "Saved as: " + savePath;
        ScreenCapture.CaptureScreenshot(savePath);

        yield return new WaitForEndOfFrame();
    }

    //another attemp save to gallery
    public void ScreenshotButton()
    {
        string folderPath = Application.dataPath + "/screenshots/";

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string fileName = "Screenshot_" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png";
        Debug.Log("Screenshot: " + folderPath + fileName);

        //StartCoroutine(DelayScreenshot(folderPath + fileName));
        StartCoroutine(CaptureScreenshot(fileName));
    }

    private string GetGalleryPath()
    {
        if (Application.platform != RuntimePlatform.Android)
            return Application.persistentDataPath;

        var jc = new AndroidJavaClass("android.os.Environment");
        var path = jc.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory",
            jc.GetStatic<string>("DIRECTORY_DCIM"))
            .Call<string>("getAbsolutePath");

        return path;
    }



    public IEnumerator DelayScreenshot (string path)
    {
        ScreenCapture.CaptureScreenshot(path, 2);

        locationText.text = "Saved as: " + path;

        for (int i = 0; i < 5; i++)
        {
            yield return null;
        }

        locationText.text = "Saved as: " + path;
        TakeScreenshot();
    }

    IEnumerator CaptureScreenshot(String imagePath)
    {
        yield return new WaitForEndOfFrame();
        //about to save an image capture
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);

        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();

        Debug.Log(" screenImage.width" + screenImage.width + " texelSize" + screenImage.texelSize);
        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();

        Debug.Log("imagesBytes=" + imageBytes.Length);

        //Save image to file
        File.WriteAllBytes(imagePath, imageBytes);

        locationText.text = "Saved as: " + imagePath;
        TakeScreenshot();
    }
}
