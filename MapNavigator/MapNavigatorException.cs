using System;

namespace MapNavigator
{
    /// <summary>
    /// I'm using this as a catch-all exception for any problem that might come
    /// about in this library. Not ideal normally, but will work for a demo project.
    /// </summary>
    public class MapNavigatorException : Exception
    {
        public MapNavigatorException()
            : this(null) { }

        public MapNavigatorException(string message)
            : base(message) { }
    }
}
