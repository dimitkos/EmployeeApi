namespace Domain.Values
{
    public class PhoneNumber
    {
        public string Phone { get; }
        public string MobilePhone { get; }

        public PhoneNumber(string phone, string mobilePhone)
        {
            Phone = phone;
            MobilePhone = mobilePhone;
        }
    }
}
