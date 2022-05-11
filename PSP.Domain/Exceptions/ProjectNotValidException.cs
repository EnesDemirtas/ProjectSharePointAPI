namespace PSP.Domain.Exceptions {

    public class ProjectNotValidException : NotValidException {

        internal ProjectNotValidException() {
        }

        internal ProjectNotValidException(string message) : base(message) {
        }

        internal ProjectNotValidException(string message, Exception inner) : base(message, inner) {
        }
    }
}