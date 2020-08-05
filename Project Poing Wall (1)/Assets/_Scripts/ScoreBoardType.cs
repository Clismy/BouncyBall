using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreBoardOffline
{
    public int adCounter = 0;
    public RowOffline[] row;
    public ScoreBoardOffline()
    {
        row = new RowOffline[10];
        for (int i = 0; i < 10; i++)
        {
            row[i] = new RowOffline();
        }

        adCounter = 3;
    }
}
[Serializable]
public class RowOffline
{
    public int score;
    public RowOffline()
    {
        score = 0;
    }
}

[Serializable]
public class ScoreBoardOnline
{
    public RowOnline[] row;
}
[Serializable]
public class RowOnline
{
    public string name;
    public int score;
}

