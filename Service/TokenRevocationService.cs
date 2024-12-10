namespace OS_and_BD_lab_2.Service
{
    public class TokenRevocationService
    {
        private readonly HashSet<string> _revokedTokens = new();

        public void RevokeToken(string token)
        {
            _revokedTokens.Add(token);
        }

        public bool IsTokenRevoked(string token)
        {
            return _revokedTokens.Contains(token);
        }
    }

}
