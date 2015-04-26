using System.ServiceModel;

namespace Commenter
{
    [ServiceContract]
    public interface IPoster
    {
        [OperationContract]
        void CleanUp();

        [OperationContract]
        Post GetPostByTitle(string title);

        [OperationContract]
        Post SubmitPost(Post post);

        [OperationContract]
        Comment SubmitComment(Comment comment);

        [OperationContract]
        void DeleteComment(Comment comment);
    }
}
