namespace RegistroUsuario.Helpers
{
    public class ApiResponseMessage<T>
    {
        public bool success { get; set; }
        public T? message { get; set; }

        public ApiResponseMessage() { }
        public ApiResponseMessage(bool success, T? message) { }
    }
}
