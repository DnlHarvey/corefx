﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;

namespace System.Net
{
    // TODO: #13607
    internal static class CookieExtensions
    {
        private static Func<Cookie, string> s_toServerStringFunc;

        public static string ToServerString(this Cookie cookie)
        {
            if (s_toServerStringFunc == null)
            {
                s_toServerStringFunc = (Func<Cookie, string>)typeof(Cookie).GetMethod("ToServerString", BindingFlags.NonPublic | BindingFlags.Instance).CreateDelegate(typeof(Func<Cookie, string>));
            }
            return s_toServerStringFunc(cookie);
        }

        private static Func<Cookie, Cookie> s_cloneFunc;

        public static Cookie Clone(this Cookie cookie)
        {
            if (s_cloneFunc == null)
            {
                s_cloneFunc = (Func<Cookie, Cookie>)typeof(Cookie).GetMethod("Clone", BindingFlags.NonPublic | BindingFlags.Instance).CreateDelegate(typeof(Func<Cookie, Cookie>));
            }
            return s_cloneFunc(cookie);
        }

        private enum CookieVariant
        {
            Unknown,
            Plain,
            Rfc2109,
            Rfc2965,
            Default = Rfc2109
        }

        private static Func<Cookie, CookieVariant> s_getVariantFunc;

        public static bool IsRfc2965Variant(this Cookie cookie)
        {
            if (s_getVariantFunc == null)
            {
                s_getVariantFunc = (Func<Cookie, CookieVariant>)typeof(Cookie).GetProperty("Variant", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod(true).CreateDelegate(typeof(Func<Cookie, CookieVariant>));
            }

            CookieVariant variant = s_getVariantFunc(cookie);

            return variant == CookieVariant.Rfc2965;
        }
    }

    internal static class CookieCollectionExtensions
    {
        private static Func<CookieCollection, Cookie, bool, int> s_internalAddFunc;

        public static int InternalAdd(this CookieCollection cookieCollection, Cookie cookie, bool isStrict)
        {
            if (s_internalAddFunc == null)
            {
                s_internalAddFunc = (Func<CookieCollection, Cookie, bool, int>)typeof(CookieCollection).GetMethod("InternalAdd", BindingFlags.NonPublic | BindingFlags.Instance).CreateDelegate(typeof(Func<CookieCollection, Cookie, bool, int>));
            }

            return s_internalAddFunc(cookieCollection, cookie, isStrict);
        }
    }
}
