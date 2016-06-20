namespace Common
{
    /// <summary>
    /// This class defines the service api endpoints
    /// </summary>
    public static class ServiceConstants
    {
        public const string V1_LeftEndpoint = "http://localhost:61244/v1/diff/left";
        public const string V1_RightEndpoint = "http://localhost:61244/v1/diff/right";
        public const string V1_ResultEndpoint = "http://localhost:61244/v1/diff";
        
        public const string V2_LeftEndpoint = "http://localhost:61244/v2/diff/left";
        public const string V2_RightEndpoint = "http://localhost:61244/v2/diff/right";
        public const string V2_ResultEndpoint = "http://localhost:61244/v2/diff";
    }
}
