using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Functions_Logic.Utilities
{
    public class Cache<T>
    {
        /// <summary>
        /// Singleton pattern for cache
        /// </summary>
        public static Cache<T> GetCache => _cache.Value;
        private static Lazy<Cache<T>> _cache = new Lazy<Cache<T>>(() => new Cache<T>());

        private Dictionary<string, T> _vault;

        private Cache()
        {
            _vault = new Dictionary<string, T>();
        }

        public void ResetCache()
        {
            _vault = new Dictionary<string, T>();

        }

        public bool TryAddItem(T item, out string key, params object[] hashCodeIdentifiers)
        {
            if (hashCodeIdentifiers == null || hashCodeIdentifiers.Any(ident => ident == null))
            {
                key = "";
                return false;
            }

            var hash = GetHashString(hashCodeIdentifiers);
            key = hash;
            if (!_vault.ContainsKey(hash))
            {
                _vault.Add(hash, item);
                return true;
            }
            return false;
        }

        public bool TryGetItem(out T item, out string key, params object[] hashCodeIdentifiers)
        {
            if (hashCodeIdentifiers == null || hashCodeIdentifiers.Any(ident => ident == null))
            {
                key = "";
                item = default;
                return false;
            }

            var hash = GetHashString(hashCodeIdentifiers);
            key = hash;
            return TryGetItem(hash, out item);
        }

        public bool TryGetItem(string hash, out T item)
        {
            if (_vault.ContainsKey(hash))
            {
                item = _vault[hash];
                return true;
            }
            item = default;
            return false;
        }

        internal static string GetHashString(params object[] hasCodeIdentifiers)
        {
            string fullString = string.Join("|", hasCodeIdentifiers.Select(GetDeepString).Where(str => !string.IsNullOrWhiteSpace(str)));
            var bytes = Encoding.UTF8.GetBytes(fullString);
            var hashAlgo = new MD5CryptoServiceProvider();
            var hashes = hashAlgo.ComputeHash(bytes);
            var guid = new Guid(hashes);
            return guid.ToString();
        }


        private static string GetDeepString(object input)
        {

            if (input is string strinInput)
            {
                return strinInput;
            }

            if (input is System.Collections.IEnumerable input1d)
            {
                string result = string.Empty;
                foreach (var item in input1d)
                {
                    if (string.IsNullOrWhiteSpace(item.ToString())) { continue; }
                    result += item.ToString() + "|";
                }
                return result.Remove(result.Length - 1, 1);
            }


            if (input is object[,] input2d)
            {
                int rows = input2d.GetLength(0);
                int cols = input2d.GetLength(1);
                string result = string.Empty;

                for (int i = 0; i < cols; i++)
                {
                    for (int j = 0; j < rows; j++)
                    {
                        string item = input2d[i, j].ToString();
                        if (string.IsNullOrWhiteSpace(item)) { continue; }
                        result += item + "|";

                    }
                }
                return result.Remove(result.Length - 1, 1);
            }

            return input.ToString();
        }
    }
}
