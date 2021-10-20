namespace InternConnect.Service
{
    public class Status
    {
        public enum StatusList
        {
            NEW_SUBMISSION,
            ACCEPTEDBYCOORDINATOR,
            ACCEPTEDBYCHAIR,
            ACCEPTEDBYDEAN,
            EMAILSENTTOCOMPANY,
            COMPANYAGREES
        }

        public enum CompanyStatusList
        {
            NEW,
            EXISTING,
            EXPIRED
        }
    }
}
