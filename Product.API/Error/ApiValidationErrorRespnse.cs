namespace Product.API.Error
{
    public class ApiValidationErrorRespnse : BaseCommonResponse
    {
        public ApiValidationErrorRespnse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
