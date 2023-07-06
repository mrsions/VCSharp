using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    public interface IVBox
    {
        object Value { get; set; }
    }

    public struct VBox<T> : IVBox
    {
        public T Value;

        object IVBox.Value
        {
            get => Value;
            set => Value = (T)value;
        }

        public VBox(T value)
        {
            Value = value;
        }
    }
}
