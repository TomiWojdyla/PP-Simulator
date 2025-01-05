using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Simulator.Maps;
using Simulator;
using System.Data;

namespace SimConsole;

public class MapVisualizer
{
    internal class MapVisulizer
    {
        private string _topLine = "";
        private string _bottomLine = "";
        private string _innerLine = "";
        //private readonly string[] _dataLines;
        public string[] dataRows;


        private Map Map { get; }

        public MapVisulizer(Map map)
        {
            this.Map = map;
            //this.dataLines = new string[this.Map.SizeY];
            this.dataRows = new string[this.Map.SizeY];
            this.InitNoDataRows();
        }

        //private void InitNoDataLines()
        //{
        //    int num1 = this.Map.SizeX - 1;
        //    int num2 = 1;
        //    List<char> charList1 = new List<char>(num2);
        //    CollectionsMarshal.SetCount<char>(charList1, num2);
        //    Span<char> span = CollectionsMarshal.AsSpan<char>(charList1);
        //    int num3 = 0;
        //    span[num3] = '┌';
        //    int num4 = num3 + 1;
        //    List<char> charList2 = charList1;
        //    for (int index = 0; index < num1; ++index)
        //        charList2.Add('┬');
        //    charList2.Add('┐');
        //    this._topLine = string.Join<char>('─', (charList2.ToArray()));
        //    charList2.Clear();
        //    charList2.Add('└');
        //    for (int index = 0; index < num1; ++index)
        //        charList2.Add('┴');
        //    charList2.Add('┘');
        //    this._bottomLine = string.Join<char>('─', (charList2.ToArray()));
        //    charList2.Clear();
        //    charList2.Add('├');
        //    for (int index = 0; index < num1; ++index)
        //        charList2.Add('┼');
        //    charList2.Add('┤');
        //    this._crossLine = string.Join<char>('─', (charList2.ToArray()));
        //}

       private void InitNoDataRows()
       {             
            int width = this.Map.SizeX -1;
            _topLine += Box.TopLeft;
            for (int i = 0; i < width; i++)
            {
                _topLine += Box.Horizontal;
                _topLine += Box.TopMid;
            }
            _topLine += Box.Horizontal;
            _topLine += Box.TopRight;

            _bottomLine += Box.BottomLeft;
            for (int i = 0; i < width; i++)
            {
                _bottomLine += Box.Horizontal;
                _bottomLine += Box.BottomMid;
            }
            _bottomLine += Box.Horizontal;
            _bottomLine += Box.BottomRight;

            _innerLine += Box.MidLeft;
            for (int i = 0; i < width; i++)
            {
                _innerLine += Box.Horizontal;
                _innerLine += Box.Cross;
            }
            _innerLine += Box.Horizontal;
            _innerLine += Box.MidRight;
        }

        //private void FillDataLines()
        //{
        //    List<char> charList = new List<char>();
        //    for (int y = 0; y < this.Map.SizeY; ++y)
        //    {
        //        for (int x = 0; x < this.Map.SizeX; ++x)
        //        {
        //            List<Creature> creatureList = this.Map.At(x, y);
        //            char ch = creatureList == null || creatureList.Count == 0 ? ' ' : (creatureList.Count <= 1 ? creatureList[0].MapSymbol : 'X');
        //            charList.Add(ch);
        //        }
        //        this._dataLines[y] = '│'.ToString() + string.Join<char>('│', charList.ToArray()) + (object)'│';
        //        charList.Clear();
        //    }
        //}

        public void InitDataRows()
        {
            for (int y = 0; y < Map.SizeY; y++)
            {
                this.dataRows[y] = Box.Vertical.ToString();
                for (int x = 0; x < Map.SizeX; x++)
                {
                    List<IMappable> creaturesInPoint = this.Map.At(x, y);
                    if (creaturesInPoint == null || creaturesInPoint.Count == 0)
                    {
                        dataRows[y] += " ";
                    }
                    else if (creaturesInPoint.Count == 1)
                    {
                        dataRows[y] += creaturesInPoint[0].MapSymbol;
                    }
                    else
                    {
                        dataRows[y] += 'X';
                    }
                    dataRows[y] += Box.Vertical;
                }
            }

        }

        public void Draw()
        {
            this.InitDataRows();
            List<string> allRows = new List<string>();
            allRows.Add(this._topLine);
            for (int j = this.Map.SizeY - 1; j > 0; j--)
            {
                allRows.Add(dataRows[j]);
                allRows.Add(this._innerLine);
            }
            allRows.Add(dataRows[0]);
            allRows.Add(_bottomLine);
            foreach (string line in allRows)
            {
                Console.WriteLine(line);
            }
        }




        //public void Draw()
        //{
        //    //this.FillDataLines();
        //    this.InitDataRows();
        //    List<string> stringList = new List<string>();
        //    stringList.Add(this._topLine);
        //    for (int j = this.Map.SizeY - 1; j > 0; j--)
        //    {
        //        stringList.Add(this._dataLines[j]);
        //        stringList.Add(this._innerLine);
        //    }
        //    stringList.Add(this._dataLines[0]);
        //    stringList.Add(this._bottomLine);
        //    stringList.ForEach(line => Console.WriteLine(line));
        //}
    }
}
