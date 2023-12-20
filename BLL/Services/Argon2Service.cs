using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class Argon2Service
    {
        private IConfiguration _Configuration;

        public Argon2Service(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public string Hash(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = Encoding.ASCII.GetBytes(_Configuration["Argon2Config:salt"]);

            // somewhere in the class definition:
            //   private static readonly RandomNumberGenerator Rng =
            //       System.Security.Cryptography.RandomNumberGenerator.Create();
            using (var rnd = RandomNumberGenerator.Create())
            {
                rnd.GetBytes(salt);
            }
            Argon2Config config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = int.Parse(_Configuration["Argon2Config:TimeCost"]),
                MemoryCost = int.Parse(_Configuration["Argon2Config:MemoryCost"]),
                Lanes = int.Parse(_Configuration["Argon2Config:Lanes"]),
                Threads = Environment.ProcessorCount, // higher than "Lanes" doesn't help (or hurt)
                Password = passwordBytes,
                Salt = salt, // >= 8 bytes if not null
                Secret = null, // from somewhere
                AssociatedData = null, // from somewhere
                HashLength = int.Parse(_Configuration["Argon2Config:HashLength"]), // >= 4
            };

            Argon2 _argon2A = new Argon2(config);
            string hashString;
            using (SecureArray<byte> hashA = _argon2A.Hash())
            {
                hashString = config.EncodeString(hashA.Buffer);
            }

            return hashString;
        }
        public bool Verify(string incomingPassword, string recordedPassword)
        {
            return Argon2.Verify(recordedPassword, incomingPassword);
        }
    }
}
