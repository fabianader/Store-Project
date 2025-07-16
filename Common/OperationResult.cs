namespace StoreProject.Common
{
    public class OperationResult
    {
        public List<string> Message { get; set; }
        public OperationResultStatus Status { get; set; }

        #region Errors
        public static OperationResult Error()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = ["Operation failed"],
            };
        }
        public static OperationResult Error(List<string> message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = message,
            };
        }
        #endregion

        #region NotFound

        public static OperationResult NotFound(List<string> message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = message,
            };
        }
        public static OperationResult NotFound()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = ["The requested information was not found."],
            };
        }

        #endregion

        #region Succsess

        public static OperationResult Success()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = ["The operation was successful."],
            };
        }
        public static OperationResult Success(List<string> message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = message,
            };
        }
        #endregion
    }
    public enum OperationResultStatus
    {
        Error = 10,
        Success = 200,
        NotFound = 404
    }
}