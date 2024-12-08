using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Simulator;

internal class Animals
{

    //Pola prywatne
    private string _description = "Unknown";

    //Właściwości automatyczne + settery/gettery
    public required string Description
    {
        get
        {
            return _description;
        }
        init
        {
            string validatedDescription = value.Trim(); //usunięcie białych znaków z początku i końca
            if (validatedDescription == "" || validatedDescription == null) //warunek jeżeli z nazwy został pusty string/null
            {
                _description = "Unknown";
            }
            else
            {
                if (validatedDescription.Length < 3)
                {
                    validatedDescription = validatedDescription.PadRight(3, '#');
                }
                else if (validatedDescription.Length > 15)
                {
                    validatedDescription = validatedDescription.Substring(0, 15).TrimEnd();
                    if (validatedDescription.Length < 3) validatedDescription.PadRight(3, '#');
                }
                if (char.IsLower(validatedDescription[0]))
                {
                    validatedDescription = char.ToUpper(validatedDescription[0]) + validatedDescription.Substring(1);
                }
                _description = validatedDescription;
            }
        }
    }
    public uint Size { get; set; } = 3;

    public string Info
    {
        get { return $"{Description} <{Size}>";  }
    }
}
