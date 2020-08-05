using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class OfflineScoreBoard : MonoBehaviour
{
    public static ScoreBoardOffline getHighScores()
    {
        if(File.Exists(Application.persistentDataPath + "/score.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + "/score.sav", FileMode.Open);

            ScoreBoardOffline scoreBoard = bf.Deserialize(fs) as ScoreBoardOffline;
            fs.Close();
            return scoreBoard;
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + "/score.sav", FileMode.Create);
            ScoreBoardOffline temp = new ScoreBoardOffline();
            bf.Serialize(fs, temp);
            fs.Close();
            return temp;
        }
    }

    public static void setHighScore(int score)
    {
        ScoreBoardOffline sb = getHighScores();
        int i = 0;
        int tempScore = 0;
        bool afterInsert = false;
        foreach (var row in sb.row)
        {
            if (score > row.score && afterInsert == false)
            {
                tempScore = sb.row[i].score;
                sb.row[i].score = score;
                afterInsert = true;
            }
            else if (afterInsert && i != 9)
            {
                int temp = sb.row[i].score;
                sb.row[i].score = tempScore;
                tempScore = temp;
            }
            else if(afterInsert && i == 9)
            {
                sb.row[i].score = tempScore;
            }
            i++;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.persistentDataPath + "/score.sav", FileMode.Create);
        bf.Serialize(fs, sb);
        fs.Close();
    }

    public static bool checkIfHighScore(int score)
    {
        ScoreBoardOffline sb = getHighScores();
        int i = 0;
        foreach (var row in sb.row)
        {
            if (score > row.score)
            {
                return true;
            }
            i++;
        }
        return false;
    }

    public static void SetAdCounter(int value)
    {
        ScoreBoardOffline sb = getHighScores();
        sb.adCounter = value;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.persistentDataPath + "/score.sav", FileMode.Create);
        bf.Serialize(fs, sb);
        fs.Close();
    }
}
