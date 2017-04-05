using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace Reto4.Services
{
    public class ServiceHelper
    {
        MobileServiceClient clienteServicio = new MobileServiceClient(@"http://xamarinchampions.azurewebsites.net");

        private IMobileServiceTable<TorneoItem> _TorneoItemTable;

        public System.Collections.Generic.List<TorneoItem> items;

        public async Task BuscarRegistros(string correo)
        {
            _TorneoItemTable = clienteServicio.GetTable<TorneoItem>();
            items = await _TorneoItemTable.Where(torneoItem => torneoItem.Email == correo).ToListAsync();
        }

        public async Task InsertarEntidad(string direccionCorreo, string reto, string AndroidId)
        {
            _TorneoItemTable = clienteServicio.GetTable<TorneoItem>();

            await _TorneoItemTable.InsertAsync(new TorneoItem
            {
                Email = direccionCorreo,
                Reto = reto,
                DeviceId = AndroidId
            });
        }
    }
}
