using Backend.Common.Dtos;

namespace Backend.ViewModels.Responses
{
    public class SuccessResponse<T>
    {
        public bool Success { get; set; }
        
        public T Payload { get; set; }
    }
}