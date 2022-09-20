using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager
{
    private string add_score_url = "https://symbolcollectionscore.000webhostapp.com/addscore.php";
    private string my_secret_key = "mySecretKey";

    public HighscoreManager() {}

    public void addElementToHighscore(string player_name, string points)
    {
        Debug.Log("Start");
       // string hash = Md5Sum(player_name + points);
        WWWForm form = new WWWForm();
        form.AddField("name", player_name);
        form.AddField("score", points);
        //form.AddField("hash", hash);
        WWW www = new WWW(add_score_url, form);
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        Debug.Log("Done");
    }

    private string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "mysecrethash";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}
