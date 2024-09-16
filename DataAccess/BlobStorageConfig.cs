namespace POC837Parser.DataAccess
{
    public static class BlobStorageConfig
    {
        //public const string ConnectionString = "UseDevelopmentStorage=true";
        public static string ConnectionString { get; set; }
        public const string SubmissionsContainerName = "submissions";
        public const string ClaimsContainerName = "claims";
    }
}
