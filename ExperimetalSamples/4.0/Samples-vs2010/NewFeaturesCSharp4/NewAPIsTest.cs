using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace NewFeaturesCSharp4
{
    class NewAPIsTest
    {
        public void Test()
        {
            string[] values = { null, String.Empty, "ABCDE", 
                          new String(' ', 20), "  \t   ", 
                          new String('\u2000', 10) };
            foreach (string value in values)
                Console.WriteLine(String.IsNullOrWhiteSpace(value));


            string[] paths = { @"d:\archives", "2001", "media", "images" };
            string fullPath = Path.Combine(paths);
            Console.WriteLine(fullPath);

            IEnumerable<string> files1 =
                Directory.EnumerateFiles(@"\\archives\2007\media",
                "*", SearchOption.AllDirectories);

            // Create a sorted set using the ByFileExtension comparer.
            SortedSet<string> mediaFiles1 =
                new SortedSet<string>(new ByFileExtension());

        }

        // Defines a comparer to create a sorted set
        // that is sorted by the file extensions.
        public class ByFileExtension : IComparer<string>
        {
            string xExt, yExt;

            CaseInsensitiveComparer caseiComp = new CaseInsensitiveComparer();

            public int Compare(string x, string y)
            {
                // Parse the extension from the file name. 
                xExt = x.Substring(x.LastIndexOf(".") + 1);
                yExt = y.Substring(y.LastIndexOf(".") + 1);

                // Compare the file extensions. 
                int vExt = caseiComp.Compare(xExt, yExt);
                if (vExt != 0)
                {
                    return vExt;
                }
                else
                {
                    // The extension is the same, 
                    // so compare the filenames. 
                    return caseiComp.Compare(x, y);
                }
            }
        }

        /// <summary>
        /// Lazy<T> lazy load
        /// </summary>
        class Person
        {
            private Lazy<byte[]> _largeProperty = new Lazy<byte[]>(()=>new byte[100]);
            public byte[] LargeProperty
            {
                get
                {
                    return _largeProperty.Value;
                }
            }
        }
    }
}
