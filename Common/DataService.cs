namespace Common
{
    /// <summary>
    /// This class just provided the sample json to post 
    /// to the endpoints for diff matching
    /// </summary>
    public static class DataService
    {
        /// <summary>
        /// Json to post to the left endpoint
        /// </summary>
        /// <returns></returns>
        public static string LeftJson()
        {
            return @"{
                            'name': 'Jinish',
                            'age': 36,
                            'certifications': ['MCP', 'CSM'],
                            'healthy': true,
                            'dob': '1980-02-08T00:00:00',
                            'issues': null,
                            'car': {
                                        'year': 2012,
                                        'model': 'Toyota Innova SUV'
                                    }
                            }";
        }

        /// <summary>
        /// Json to post to the right endpoint
        /// </summary>
        /// <returns></returns>
        public static string RightJson()
        {
            return @"{
                            'name': 'Donald',
                            'age': 35,
                            'certifications': ['MCP', 'CSM'],
                            'healthy': false,
                            'dob': '1980-02-08T00:00:00',
                            'issues': null,
                            'car': {
                                        'year': 2011,
                                        'model': 'Toyota Innova SUV'
                                    }
                            }";
        }
    }
}
