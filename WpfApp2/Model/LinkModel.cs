namespace WpfApp2.Model
{
    public class LinkModel
    {
        public ObjectLinkableModel ObjectLinkableStart { get; set; }
        public ObjectLinkableModel ObjectLinkableEnd { get; set; }
        // info from the popUp, to change later with something more complex
        public string Info { get; set; }
        public LinkModel(ObjectLinkableModel start, ObjectLinkableModel end, string info) { 
            ObjectLinkableStart = start;
            ObjectLinkableEnd = end;
            Info = info;
        }
    }
}