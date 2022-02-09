using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

public class TextTranslator
{
    private string language;
    private Dictionary<string, string> dictionary;
    private string field;

    public TextTranslator(string language = "english", string field = null)
    {
        this.language = language;
        Field = field;
    }

    public string Language
    {
        get => language;
        set
        {
            language = value;
            GetDictionary();
        }
    }
    public string Field
    {
        get => field;
        set
        {
            field = value;
            GetDictionary();
        }
    }
    private void GetDictionary()
    {
        string name = (field == null ? "" : field + "/") + language + ".json";

        if (File.Exists(name))
        {
            name = File.ReadAllText(name);
            dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(name);
        }
        else 
            throw new System.Exception(string.Format("file {0} was not found", name));

        Dictionary<string, string> newDictionary = new Dictionary<string, string>();
        foreach (string element in dictionary.Keys)
            newDictionary[element.ToLower()] = dictionary[element];
        dictionary = newDictionary;
    }

    public string GetTranslatedText(string text) => dictionary[text.ToLower()];
}
