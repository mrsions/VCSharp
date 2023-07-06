using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCSharp
{
    public struct ObjPtr
    {
        private List<object?> objects;
        private int cursor;

        internal ObjPtr(List<object?> objects, int cursor) : this()
        {
            this.objects = objects;
            this.cursor = cursor;
        }

        public object? Data
        {
            get => cursor < objects.Count ? objects[cursor] : null;
            set
            {
                if (cursor < objects.Count) objects[cursor] = value;
                else objects.Add(value);
            }
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is ObjPtr ptr)
            {
                return this == ptr;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return objects.GetHashCode() ^ cursor;
        }

        public static bool operator ==(ObjPtr lhs, ObjPtr rhs)
        {
            return lhs.objects == rhs.objects && lhs.cursor == rhs.cursor;
        }
        public static bool operator !=(ObjPtr lhs, ObjPtr rhs)
        {
            return lhs.objects != rhs.objects || lhs.cursor != rhs.cursor;
        }
        public static bool operator <(ObjPtr lhs, ObjPtr rhs)
        {
            return lhs.objects == rhs.objects && lhs.cursor < rhs.cursor;
        }
        public static bool operator >(ObjPtr lhs, ObjPtr rhs)
        {
            return lhs.objects == rhs.objects && lhs.cursor > rhs.cursor;
        }
        public static bool operator <=(ObjPtr lhs, ObjPtr rhs)
        {
            return lhs.objects == rhs.objects && lhs.cursor <= rhs.cursor;
        }
        public static bool operator >=(ObjPtr lhs, ObjPtr rhs)
        {
            return lhs.objects == rhs.objects && lhs.cursor >= rhs.cursor;
        }
        public static ObjPtr operator -(ObjPtr lhs, int move)
        {
            return new ObjPtr(lhs.objects, lhs.cursor + move);
        }
        public static ObjPtr operator +(ObjPtr lhs, int move)
        {
            return new ObjPtr(lhs.objects, lhs.cursor + move);
        }
        public static ObjPtr operator ++(ObjPtr lhs)
        {
            return new ObjPtr(lhs.objects, lhs.cursor + 1);
        }
        public static ObjPtr operator --(ObjPtr lhs)
        {
            return new ObjPtr(lhs.objects, lhs.cursor - 1);
        }
    }
}
