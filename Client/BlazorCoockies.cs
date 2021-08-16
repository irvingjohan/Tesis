using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using RIEGO.Shared.Entidades;

namespace RIEGO.Client
{
    public class BlazorCoockies
    {
        public string UserName { get; set; }
        public string EsAdmin { get; set; }
        public string Ver { get; set; }
        public string Crear { get; set; }
        public string Editar { get; set; }
        public string Borrar { get; set; }
        public string VerUsuarios { get; set; }
        public string CrearUsuarios { get; set; }
        public string EditarUsuarios { get; set; }
        public string BorrarUsuarios { get; set; }
        public static bool isLoggedIn { get; set; }
        public string IDUserEmpresa { get; set; }
        public string IDUserSucursal { get; set; }
        public string VerReportes { get; set; }

        public async Task CoockieSetter(ISessionStorageService sessionStorage, string usrName, string permiso, string idEmpresa, string idSucursal, string verReportes, List<Permisos> permisos = null)
        {
            switch (permiso)
            {
                case "Admin":
                    await sessionStorage.SetItemAsync("UserName", usrName).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("EsAdmin", "true").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Ver", "true").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Crear", "true").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Editar", "true").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Borrar", "true").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("VerUsuarios", "true").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("CrearUsuarios", "true").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("EditarUsuarios", "true").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("BorrarUsuarios", "true").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("UserEmpresa", idEmpresa).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("UserSucursal", idSucursal).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("VerReportes", "true").ConfigureAwait(true);/***/
                    break;
                case "Nada":
                    if (usrName != null)
                        await sessionStorage.SetItemAsync("UserName", usrName).ConfigureAwait(true);
                    else
                        await sessionStorage.SetItemAsync("UserName", "Nadie").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("EsAdmin", "false").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Ver", "false").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Crear", "false").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Editar", "false").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Borrar", "false").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("VerUsuarios", "false").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("CrearUsuarios", "false").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("EditarUsuarios", "false").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("BorrarUsuarios", "false").ConfigureAwait(true);

                    if (idEmpresa != null)
                        await sessionStorage.SetItemAsync("UserEmpresa", idEmpresa).ConfigureAwait(true);
                    else
                        await sessionStorage.SetItemAsync("UserEmpresa", 0).ConfigureAwait(true);
                    if (idSucursal != null)
                        await sessionStorage.SetItemAsync("UserSucursal", idSucursal).ConfigureAwait(true);
                    else
                        await sessionStorage.SetItemAsync("UserSucursal", 0).ConfigureAwait(true);

                    await sessionStorage.SetItemAsync("VerReportes", "false").ConfigureAwait(true);/***/
                    break;

                default:
                    var ver = permisos.Where(x => x.Descripcion == "Ver").Select(x => x.Valor).FirstOrDefault();
                    var crear = permisos.Where(x => x.Descripcion == "Crear").Select(x => x.Valor).FirstOrDefault();
                    var editar = permisos.Where(x => x.Descripcion == "Editar").Select(x => x.Valor).FirstOrDefault();
                    var borrar = permisos.Where(x => x.Descripcion == "Borrar").Select(x => x.Valor).FirstOrDefault();
                    var verUsuarios = permisos.Where(x => x.Descripcion == "VerUsuarios").Select(x => x.Valor).FirstOrDefault();
                    var crearUsuarios = permisos.Where(x => x.Descripcion == "CrearUsuarios").Select(x => x.Valor).FirstOrDefault();
                    var editarUsuarios = permisos.Where(x => x.Descripcion == "EditarUsuarios").Select(x => x.Valor).FirstOrDefault();
                    var borrarUsuarios = permisos.Where(x => x.Descripcion == "BorrarUsuarios").Select(x => x.Valor).FirstOrDefault();

                    await sessionStorage.SetItemAsync("UserName", usrName).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("EsAdmin", "false").ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Ver", ver.ToString()).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Crear", crear.ToString()).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Editar", editar.ToString()).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("Borrar", borrar.ToString()).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("VerUsuarios", verUsuarios.ToString()).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("CrearUsuarios", crearUsuarios.ToString()).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("EditarUsuarios", editarUsuarios.ToString()).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("BorrarUsuarios", borrarUsuarios.ToString()).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("UserEmpresa", idEmpresa).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("UserSucursal", idSucursal).ConfigureAwait(true);
                    await sessionStorage.SetItemAsync("VerReportes", verReportes).ConfigureAwait(true);/***/
                    break;
            }
        }

        //string GetPermisoNombre(Permisos item)
        //{
        //    if (item.Activo)
        //    {
        //        return item.Descripcion;
        //    }
        //    else
        //    {
        //        return item.Descripcion;
        //    }
        //}
        //string GetPermisoValor(Permisos item)
        //{
        //    if (item.Activo)
        //    {
        //        return item.Valor.ToString();
        //    }
        //    else
        //    {
        //        return item.Valor.ToString();
        //    }
        //}

        public async Task CoockieGetter(ISessionStorageService sessionStorage)
        {
            UserName = await sessionStorage.GetItemAsync<string>("UserName").ConfigureAwait(true);
            EsAdmin = await sessionStorage.GetItemAsync<string>("EsAdmin").ConfigureAwait(true);
            Ver = await sessionStorage.GetItemAsync<string>("Ver").ConfigureAwait(true);
            Crear = await sessionStorage.GetItemAsync<string>("Crear").ConfigureAwait(true);
            Editar = await sessionStorage.GetItemAsync<string>("Editar").ConfigureAwait(true);
            Borrar = await sessionStorage.GetItemAsync<string>("Borrar").ConfigureAwait(true);
            VerUsuarios = await sessionStorage.GetItemAsync<string>("VerUsuarios").ConfigureAwait(true);
            CrearUsuarios = await sessionStorage.GetItemAsync<string>("CrearUsuarios").ConfigureAwait(true);
            EditarUsuarios = await sessionStorage.GetItemAsync<string>("EditarUsuarios").ConfigureAwait(true);
            BorrarUsuarios = await sessionStorage.GetItemAsync<string>("BorrarUsuarios").ConfigureAwait(true);
            IDUserEmpresa = await sessionStorage.GetItemAsync<string>("UserEmpresa").ConfigureAwait(true);
            IDUserSucursal = await sessionStorage.GetItemAsync<string>("UserSucursal").ConfigureAwait(true);
            VerReportes = await sessionStorage.GetItemAsync<string>("VerReportes").ConfigureAwait(true);
        }
    }
}
