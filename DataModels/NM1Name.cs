namespace POC837Parser.DataModels
{
    public class NM1Name
    {
        public string EntityIdentifierCode { get; set; }
        public string EntityIdentifierDescription { get; set; }
        public string EntityTypeQualifier { get; set; }
        public string LastNameOrOrgName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NamePrefix { get; set; }
        public string NameSuffix { get; set; }
        public string IdentificationCodeQualifier { get; set; }
        public string IdentificationCode { get; set; }
        public Address Address { get; set; }         
    }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateOrProvinceCode { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string LocationQualifier { get; set; }
        public string LocationIdentifier { get; set; }

    }


}
