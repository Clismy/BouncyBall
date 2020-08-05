using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

public class OnlineScoreBoard : MonoBehaviour
{
    public static bool checkIfHighScore(int score, int limit)
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("score", Convert.ToString(score));
                byte[] responsebytes = client.UploadValues("https://programcrew.000webhostapp.com/?q=checkIfHighScore&limit=" + limit, "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);
                //Debug.Log(responsebody);
                if (responsebody == "true")
                {
                    return true;
                }
            }
        }
        catch
        {
            return false;
        }
        return false;
    }


    public static void setHighScore(string name, int score)
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("score", Convert.ToString(score));
                reqparm.Add("name", name);
                byte[] responsebytes = client.UploadValues("https://programcrew.000webhostapp.com/?q=setHighScore", "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);
                //Debug.Log(responsebody);
            }
        }
        catch
        {
            Debug.Log("Could not connect.");
        }
    }
    public static ScoreBoardOnline getHighScores(int limit)
    {
        try
        {
            using (WebClient client = new WebClient())
            {
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                byte[] responsebytes = client.UploadValues("https://programcrew.000webhostapp.com/?q=getHighScores&limit="+limit, "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);
                //Debug.Log(responsebody);
                return JsonUtility.FromJson<ScoreBoardOnline>(responsebody);
            }
        }
        catch
        {
            
            Debug.Log("Could not connect.");
            return null;
        }
    }
}
