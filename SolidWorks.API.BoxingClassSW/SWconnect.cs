using SolidWorks.Interop.sldworks;

namespace SolidWorks.API.BoxingSW
{
    public class SWconnect
    {
        public SldWorks SwObject { get;  set; }
        public SWconnect()
        {
            SwObject = new SldWorks();
            SwObject.Visible = true;
        }
        public void Disconnect( )
        {
            SwObject = null;
        }
    }
}
