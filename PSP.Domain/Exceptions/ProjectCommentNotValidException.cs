namespace PSP.Domain.Exceptions
{

    public class ProjectCommentNotValidException : NotValidException
    {

        internal ProjectCommentNotValidException()
        {
        }

        internal ProjectCommentNotValidException(string message) : base(message)
        {
        }

        internal ProjectCommentNotValidException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}