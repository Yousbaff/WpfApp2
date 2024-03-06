using WpfApp2.Model;

namespace WpfApp2
{
    public interface ILinkCreator
    {
        void AddNewLink(string popUpResponse, ObjectLinkableModel object1, ObjectLinkableModel object2);
    }
}