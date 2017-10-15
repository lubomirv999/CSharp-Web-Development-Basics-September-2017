namespace WebServer.Server.Common
{
    using System;

    public static class CommonValidator
    {
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}