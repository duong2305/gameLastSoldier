using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using JetBrains.Annotations;
using Unity.Properties;

public class CreditHandler : MonoBehaviour
{
    [SerializeField] private string path = "Assets/Scripts/Credits.txt";
    [SerializeField] public Font m_Font;
    [SerializeField] private Color headerColor = Color.black;
    [SerializeField] private Color nameColor = Color.white;
    [SerializeField] private int headerSize = 35;
    [SerializeField] private int nameSize = 25;
    [SerializeField] private float scrollSpeed = 10;
    [SerializeField] private int screenSpaceInDivisions = 8;

    List<string> headers = new List<string>();
    List<List<string>> titles = new List<List<string>>();
    List<GameObject> creditsTexts = new List<GameObject>();
    // Start is called before the first frame update


    public void Awake()
    {
        StreamReader reader = new StreamReader(path);
        string line = "";
        bool newStart = false;
        while ((line = reader.ReadLine()) != null)
        {
            string firstCharacter = line.Substring(0, 1);
            bool isIgnore = firstCharacter.Equals("#");
            bool isHeader = firstCharacter.Equals("!");
            if (isIgnore)
            {
                //Do nothing
            }
            else if (isHeader)
            {
                newStart = true;
                headers.Add(line.Substring(1));
            }
            else
            {
                if (newStart)
                {
                    titles.Add(new List<string>());
                    newStart = false;
                }
                titles[titles.Count - 1].Add(line);
            }
        }
        reader.Close();
    }
    void Start()
    {
        Vector3 lastPosition = new Vector3(Screen.width * 0.5f, 0, 0);
        for(int i=0; i<headers.Count; i++)
        {
            GameObject newObj = newText(headers[i], true);
            Vector3 nextPosition = new Vector3(Screen.width * 0.5f, lastPosition.y - (Screen.height / screenSpaceInDivisions), 0);
            newObj.transform.position = nextPosition;
            lastPosition = nextPosition;
            creditsTexts.Add(newObj);
            for(int j=0; j < titles[i].Count; j++)
            {
                nextPosition = new Vector3(Screen.width * 0.5f, lastPosition.y - (Screen.height / screenSpaceInDivisions), 0);
                GameObject oObj = newText(titles[i][j], false);
                oObj.transform.position = nextPosition;
                creditsTexts.Add(oObj);
                lastPosition = nextPosition;
            }
        }
    }

    public GameObject newText(string labelText, bool isHeader)
    {
        GameObject textObj = new GameObject(labelText);
        textObj.transform.SetParent(this.transform);
        Text myText;
        myText = textObj.AddComponent<Text>();
        myText.text = labelText;
        myText.font = m_Font;
        myText.horizontalOverflow = HorizontalWrapMode.Overflow;
        myText.alignment = TextAnchor.MiddleCenter;
        if(isHeader)
        {
            myText.fontStyle = FontStyle.Bold;
            myText.color = headerColor;
            myText.fontSize = headerSize;
        }
        else
        {
            myText.color = nameColor;
            myText.fontSize = nameSize;
        }
        textObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        textObj.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.25f, 0);

        return textObj;
    }


    

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<creditsTexts.Count; i++)
        {
            if (creditsTexts[i] != null)
            {
                creditsTexts[i].transform.position = new Vector3(creditsTexts[i].transform.position.x, creditsTexts[i].transform.position.y + scrollSpeed, 0);
                if (creditsTexts[i].transform.position.y > Screen.height * 2)
                {
                    Destroy(creditsTexts[i]);
                    creditsTexts[i] = null;
                }
            }
        }
    }
}
