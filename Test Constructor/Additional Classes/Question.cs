using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using Test_Constructor.Additional_Classes.SerializationDeserializationQuestion;
using Test_Constructor.Additional_Classes;
using System.Drawing;

public class Question
{
    public string textOfQuestion { get; set; }
    public decimal points { get; set; }
    public List<Answer> answers { get; set; }

    [XmlIgnore]
    public Image image
    {
        get
        {
            if (!string.IsNullOrEmpty(imageBase64))
            {
                return FromBase64String(imageBase64);
            }
            return null;
        }
        set
        {
            if (value != null)
            {
                ConvertingImageToBase64 convertingImageToBase64 = new ConvertingImageToBase64();
                imageBase64 = convertingImageToBase64.ImageToBase64(value, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }

    [XmlElement("imageBase64")]
    public string imageBase64 { get; set; }

    public Question()
    {
        CreateListOfAnswers();
    }

    public Question(decimal points)
    {
        this.points = points;
        CreateListOfAnswers();
    }

    private void CreateListOfAnswers()
    {
        answers = new List<Answer>();
    }

    public void addAnswer(Answer answer)
    {
        answers.Add(answer);
    }

    private Image FromBase64String(string base64String)
    {
        byte[] bytes = Convert.FromBase64String(base64String);
        using (var ms = new System.IO.MemoryStream(bytes))
        {
            return Image.FromStream(ms);
        }
    }
}