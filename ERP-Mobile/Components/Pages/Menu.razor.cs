using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Mobile.Components.Pages
{
    public partial class Menu
    {
        [Inject] private NavigationManager Nav { get; set; } = default!;
        public void OnMenuClicked(string path)
        {
            Nav.NavigateTo(path);
        }
    }
}
