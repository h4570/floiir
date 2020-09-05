namespace WebApi.Dtos.Internal
{
    public class DataOrStatusCodeDto<T>
    {

        /// <summary>
        /// Use when API response should return 200 status code (success)
        /// </summary>
        /// <param name="data"></param>
        public DataOrStatusCodeDto(T data)
        {
            Succeed = true;
            Data = data;
        }

        /// <summary>
        /// Use when API response should fail status code
        /// </summary>
        /// <param name="statusCode">Prefer to use X60-X99 which seems to be not used. Example 480</param>
        /// <param name="message">Message which will be displayed in API response</param>
        public DataOrStatusCodeDto(int statusCode, string message)
        {
            Succeed = false;
            FailStatusCode = statusCode;
            FailStatusMessage = message;
        }

        /// <summary>
        /// Cast method. Clones FailStatusCode and FailStatusMessage to new dto with another T param
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public DataOrStatusCodeDto<T2> ToFailType<T2>()
        {
            return new DataOrStatusCodeDto<T2>(FailStatusCode, FailStatusMessage);
        }

        public bool Succeed { get; private set; }
        public T Data { get; private set; }
        public int FailStatusCode { get; private set; }
        public string FailStatusMessage { get; private set; }

    }
}
