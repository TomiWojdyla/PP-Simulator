using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

internal class Creature
{
    //Pola prywatne
    private string _name = "Unknown";
    private int _level;

    //Właściwości automatyczne + settery/gettery
    public string Name 
    { 
        get 
        {  
            return _name; 
        }
        init
        {
            string validatedName = value.Trim(); //usunięcie białych znaków z początku i końca
            if (validatedName == "" || validatedName == null ) //warunek jeżeli z nazwy został pusty string/null
            {
                _name = "Unknown";
            }
            else
            {
                if (validatedName.Length < 3)
                {
                    validatedName = validatedName.PadRight(3, '#');
                }
                else if (validatedName.Length > 25) 
                {
                    validatedName = validatedName.Substring(0, 25).TrimEnd();
                    if (validatedName.Length < 3) validatedName.PadRight(3, '#');
                }
                if (char.IsLower(validatedName[0]))
                {
                    validatedName = char.ToUpper(validatedName[0]) + validatedName.Substring(1);
                }
                _name = validatedName;
            }
        } 
    }
    public int Level 
    { 
        get
        {
            return _level;
        }
        init
        {
            if (value < 1)
            {
                _level = 1;
            }
            else if (value > 10)
            {
                _level = 10;
            }
            else
            {
                _level = value;
            }
        }
    }

    //Konstruktor z parametrami
    public Creature(string name,int level = 1)
    {
        Name = name;
        Level = level;
    }

    //Konstruktor bez parametrów
    public Creature()
    {
        //Nic nie wykonuje
    }

    public void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Name}, my level is {Level}");
    }

    public string Info
    {
        get { return $"{Name} [{Level}]"; }
    }

    public void Upgrade()
    {
        if (_level<10)
        {
            _level++;
        }
    }
}
