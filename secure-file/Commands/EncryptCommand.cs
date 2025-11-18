using System;
using System.Security.Cryptography;

namespace AppVeyor.Tools.SecureFile.Commands
{
    /// <summary>
    /// Provides a command to encrypt a file.
    /// </summary>
    class EncryptCommand : SymmetricAlgorithmCommand
    {
        /// <summary>
        /// Identifies the default length of any salt generated.
        /// </summary>
        public const int DefaultSaltLength = 38;

        protected override void OnAfterExecute(ApplicationContext context)
        {
            Console.WriteLine($"Salt: {context.Salt}");

            base.OnAfterExecute(context);
        }

        protected override ICryptoTransform CreateTransform(SymmetricAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            return algorithm.CreateEncryptor();
        }

        protected override byte[] GetSaltBytes(ApplicationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var bytes = new byte[DefaultSaltLength];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetNonZeroBytes(bytes);
            }

            context.Salt = Convert.ToBase64String(bytes);

            return bytes;
        }
    }
}
