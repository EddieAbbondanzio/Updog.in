namespace Updog.Domain.Tests {
    public sealed class MockPasswordHasher : IPasswordHasher {
        public string Hash(string password) => password;
        public bool Verify(string password, string hash) => hash == password;
    }
}