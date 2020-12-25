using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;

namespace SolidWorks.API.BoxingSW
{
    /// <summary>
    /// Класс Свойства пользовательское свойство из св-в файла SolidWorks
    /// </summary>
    public class SwProperty : IEquatable<SwProperty>
    {
        public SwProperty(string name, object value, swCustomInfoType_e type)
        {
            this.Name = name; // имя свойства
            this.Value = (string)value; // значение свойства
            this.Type = type; // тип свойства
        }

        public string Name { get;  }
        public string Value { get; set; }
        public swCustomInfoType_e Type { get;  }

        public override bool Equals( object obj )
        {
            return Equals( obj as SwProperty );
        }

        public bool Equals( SwProperty other )
        {
            return other != null &&
                   Name == other.Name &&
                   Value == other.Value;
        }

        public override int GetHashCode( )
        {
            var hashCode = -244751520;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode( Name );
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode( Value );
            return hashCode;
        }

        public static bool operator ==( SwProperty property1, SwProperty property2 )
        {
            return EqualityComparer<SwProperty>.Default.Equals( property1, property2 );
        }

        public static bool operator !=( SwProperty property1, SwProperty property2 )
        {
            return !(property1 == property2);
        }
    }



}

