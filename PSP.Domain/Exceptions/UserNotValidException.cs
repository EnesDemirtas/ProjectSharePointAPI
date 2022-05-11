namespace PSP.Domain.Exceptions {

    public class UserNotValidException : NotValidException {

        internal UserNotValidException() {
        }

        internal UserNotValidException(string message) : base(message) {
        }

        internal UserNotValidException(string message, Exception inner) : base(message, inner) {
        }
    }
}