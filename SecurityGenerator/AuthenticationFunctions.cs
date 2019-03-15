using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGenerator
{
    public class AuthenticationFunctions
    {

        #region Private Variables
        private readonly int _saltByteSize = 24;
        private readonly int _hashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
        private readonly int _pbkdf2Iterations = 1000;
        private readonly int _iterationIndex = 0;
        private readonly int _saltIndex = 1;
        private readonly int _pbkdf2Index = 2;
        #endregion

        /// <summary>
        /// Default Constructor for Authentication Functions class
        /// </summary>
        public AuthenticationFunctions()
        {
            _saltByteSize = 24;
            _hashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
            _pbkdf2Iterations = 1000;
            _iterationIndex = 0;
            _saltIndex = 1;
            _pbkdf2Index = 2;
        }
        /// <summary>
        /// Constructor for Authentication Functions class.  Sets the constants that are used during hashing and validating passwords
        /// </summary>
        /// <param name="saltByteSize"></param>
        /// <param name="hashByteSize"></param>
        /// <param name="interations"></param>
        /// <param name="iterationIndex"></param>
        /// <param name="saltIndex"></param>
        /// <param name="pbkd2Index"></param>
        public AuthenticationFunctions(int saltByteSize, int hashByteSize, int interations, int iterationIndex, 
            int saltIndex, int pbkd2Index)
        {
            _saltByteSize = saltByteSize;
            _hashByteSize = hashByteSize; 
            _pbkdf2Iterations = interations;
            _iterationIndex = iterationIndex;
            _saltIndex = saltIndex;
            _pbkdf2Index = pbkd2Index;
        }

        public string HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[_saltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, _pbkdf2Iterations, _hashByteSize);
            return _pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            try
            {
                char[] delimiter = { ':' };
                var split = correctHash.Split(delimiter);
                var iterations = Int32.Parse(split[_iterationIndex]);
                var salt = Convert.FromBase64String(split[_saltIndex]);
                var hash = Convert.FromBase64String(split[_pbkdf2Index]);

                var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
                return SlowEquals(hash, testHash);
            }
            catch (Exception e)
            {
                throw new Exception("An Error occurred while validating password", e);
            }
        }

        private bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
