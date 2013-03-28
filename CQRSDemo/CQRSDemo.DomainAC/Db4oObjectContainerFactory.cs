using Db4objects.Db4o;

namespace CQRSDemo.DomainAC
{
    public class Db4oObjectContainerFactory
    {
        private static IObjectServer _server;
        private static IObjectContainer _object_container;

        public Db4oObjectContainerFactory(string container_path) 
        {
            if (_server == null) {
                Connect(container_path);
            }
        }
 
        private void Connect(string container_path) {
            _server = Db4oFactory.OpenServer(container_path, 0);            
        }
         
        public IObjectContainer Create()
        {
            return _server.OpenClient();
        }     
    }
}
