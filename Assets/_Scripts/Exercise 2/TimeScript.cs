using System;
using UnityEngine;
using TMPro;

public class TimeScript : MonoBehaviour
{
    public TMP_InputField inputText;
    public TextMeshProUGUI outputText;
    

    public void ConvertButton()
    {
        Debug.Log("ConvertButton" + inputText.text);

        outputText.text = ConvertTime(inputText.text);
    }

    private string ConvertTime(string inputString)
    {
        string outputString = "";

        DateTime parsed;

        bool valid = DateTime.TryParseExact(inputString, "hh:mm:sstt",
                                            System.Globalization.CultureInfo.InvariantCulture,
                                            System.Globalization.DateTimeStyles.None,
                                            out parsed);
        if (valid)
        {
            DateTime dateTime = DateTime.Parse(inputString, System.Globalization.CultureInfo.CurrentCulture);
            outputString = dateTime.ToString("HH:mm:ss");
            Debug.Log("valid time");
        }
        else
        {
            outputString = "Invalid input format";
            Debug.Log("invalid time");
        }

        return outputString;
    }
}
