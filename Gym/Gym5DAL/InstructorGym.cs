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
    
    public partial class InstructorGym
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InstructorGym()
        {
            this.PersonaGyms = new HashSet<PersonaGym>();
        }
    
        public string idInstructor { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string telefono { get; set; }
        public string usuario { get; set; }
        public string idNombreEmpresa { get; set; }
    
        public virtual LogEmpresa LogEmpresa { get; set; }
        public virtual LogUsuario LogUsuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonaGym> PersonaGyms { get; set; }
    }
}