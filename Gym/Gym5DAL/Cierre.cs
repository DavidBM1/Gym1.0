//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gym5DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cierre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cierre()
        {
            this.CierreProductoes = new HashSet<CierreProducto>();
        }
    
        public string idCierre { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string usuario { get; set; }
        public string idNombreEmpresa { get; set; }
    
        public virtual LogEmpresa LogEmpresa { get; set; }
        public virtual LogUsuario LogUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CierreProducto> CierreProductoes { get; set; }
    }
}
