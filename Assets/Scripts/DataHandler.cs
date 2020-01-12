using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataHandler
{


    public static List<string[]> readData()
    {
        string folder = Convert.ToString(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
        bool exists = System.IO.Directory.Exists(folder + @"\FlappyPegasus");

        if (exists)
        {
            List<string[]> data = new List<string[]>();
            StreamReader reader = new StreamReader(folder + @"\FlappyPegasus\SaveRecord.txt");
            while (reader.Peek() != -1)
                data.Add(reader.ReadLine().Split(','));
            reader.Close();
            reader.Dispose();
            return data;
        }
        else
        {
            System.IO.Directory.CreateDirectory(folder + @"\FlappyPegasus");
            return createDefaultFile();
        }
    }

    public static void insertNewItem(ref List<string[]> data, string[] newItem)
    {
        System.Globalization.NumberStyles style = System.Globalization.NumberStyles.Integer;
        //Check minimum border
        Debug.Log("newItem: " + newItem[1]);
        Debug.Log("data: " + data[data.Count - 1][1]);
        if (Int32.Parse(newItem[1], style) < Int32.Parse(data[data.Count - 1][1], style))
            return;

        //Check user in records
        sbyte i = 0;
        while (i < data.Count && data[i][0] != newItem[0])
            ++i;
        bool isOldDeleted = false;

        //If new record greater then past, delete old
        if (i < data.Count && Int32.Parse(newItem[1], style) > Int32.Parse(data[i][1], style))
        {
            data.RemoveAt(i);
            isOldDeleted = true;
        }
        else if (i < data.Count) //New record less then past
            return;

        //Find place for new record
        for (i = 0; i < data.Count; ++i)
        {
            if (Int32.Parse(data[i][1], style) < Int32.Parse(newItem[1], style))
            {
                data.Insert(i, newItem);
                break;
            }
        }
        if (!isOldDeleted)
            data.RemoveAt(data.Count - 1);
    }

    public static void writeData(ref List<string[]> data)
    {
        StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\FlappyPegasus\SaveRecord.txt", false);
        for (int i = 0; i < data.Count; ++i)
            writer.WriteLine($"{data[i][0]},{data[i][1]}");
        writer.Close();
    }

    private static List<string[]> createDefaultFile()
    {
        List<string[]> dataList = new List<string[]>();
        Debug.Log("createFail()");
        using (StreamWriter fstream = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\FlappyPegasus\SaveRecord.txt", false))
        {
            for (int i = 1, j = 100; i < 10; ++i, j -=10)
            {
                fstream.Write($"Player{i},{j}\n");
                dataList.Add(new string[] { $"Player{i}", $"{j}" });
            }
        }
        return dataList;

    }

}
