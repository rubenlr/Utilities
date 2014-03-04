namespace Utilities.Common.DataTypes
{
    public struct CreditCard
    {
        private readonly string _cardNumber;

        public CreditCard(string card)
        {
            _cardNumber = card.Replace("-", "");
        }

        public static implicit operator CreditCard(string cartao)
        {
            return new CreditCard(cartao);
        }

        public override int GetHashCode()
        {
            return _cardNumber.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is string)
                return Equals(obj as string);

            return false;
        }

        public static bool operator ==(CreditCard a, CreditCard b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(CreditCard a, CreditCard b)
        {
            return !a.Equals(b);
        }

        public bool Equals(CreditCard creditCard)
        {
            return Equals(creditCard._cardNumber, _cardNumber);
        }

        public string Bin
        {
            get { return _cardNumber.Substring(0, 6); }
        }

        public string Truncado
        {
            get
            {
                var bin = string.Empty;
                var clientIdentification = string.Empty;

                if (_cardNumber.Length >= 6)
                    bin = _cardNumber.Substring(0, 6);

                if (_cardNumber.Length >= 10)
                    clientIdentification = _cardNumber.Substring(_cardNumber.Length - 4, 4);

                return bin + clientIdentification;
            }
        }
    }
}