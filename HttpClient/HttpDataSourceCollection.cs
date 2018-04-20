/*
 * This work is licensed under the terms of the MIT license.
 * For a copy, see <https://opensource.org/licenses/MIT>.
 */
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace CJO.Web.Http
{
    public class HttpDataSourceCollection : Collection<HttpDataSource>
    {
        public void Add(HttpDataSourceCollection dataItems)
        {
            foreach (HttpDataSource dataItem in dataItems)
            {
                base.Add(dataItem);
            }
        }

        /// Adds new RestDataItem to the list
        public void Add(string name, byte[] value)
        {
            this.Add(name, value, null);
        }

        /// Adds new RestDataItem to the list
        public void Add(string name, byte[] value, string fileName)
        {
            base.Add(new HttpDataSource(name, value, fileName));
        }

        /// Adds the supplied FileInfo
        public void Add(string name, FileInfo file, string contentType)
        {
            base.Add(new HttpDataSource(name, file, contentType));
        }

        /// Adds new RestDataItem to the list
        public void Add(string name, byte[] value, string fileName, string contentType)
        {
            base.Add(new HttpDataSource(name, value, contentType));
        }

        /// Adds a file to the current collection
        public void AddFile(string fileName, string contentType)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File not found", fileName);
            }
            base.Add(new HttpDataSource(fileName, new FileInfo(fileName), contentType));
        }
    }
}


